using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TDevs.Core.Infrastructure;
using TDevs.Domain;
using TDevs.Domain.ViewModel;

namespace TDevs.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository<Agent> agentRepository;
        private readonly IRepository<Member> memberRepository;
        private readonly IRepository<Medallion> medallionRepository;
        public AgentService(IRepository<Agent> agentRepository, IRepository<Member> memberRepository, IRepository<Medallion> medallionRepository)
        {
            this.agentRepository = agentRepository;
            this.memberRepository = memberRepository;
            this.medallionRepository = medallionRepository;
        }

        public IList<Agent> Get()
        {
            return agentRepository.Get.ToList();
        }

        public Agent Get(Guid Id)
        {
            return agentRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public Agent Add(Agent agent)
        {
            agentRepository.Add(agent);
            return agent;
        }

        public Agent Update(Agent agent)
        {
            agentRepository.Update(agent);
            return agent;
        }

        public bool Remove(Guid Id)
        {
            var agent = Get(Id);
            if (agent == null) return false;
            agentRepository.Remove(agent);
            return true;
        }


        public IList<SearchAgentResult> SearchAgentByCriterial(AgentCriterial criterial)
        {
            List<SearchAgentResult> listResult = new List<SearchAgentResult>();
            if (criterial != null)
            {
                var listAgent = (agentRepository.Get.Where(c=>!c.IsDeleted)
                    .Include(m => m.Member)
                    .Where(c => (c.FirstName.Contains(criterial.FirstName) || String.IsNullOrEmpty(criterial.FirstName)) &&
                    (c.LastName.Contains(criterial.LastName) || String.IsNullOrEmpty(criterial.LastName)) &&
                    (c.ChaufferLic.Equals(criterial.ChaufferLic) || String.IsNullOrEmpty(criterial.ChaufferLic)))).ToList();

                var listMedallion = medallionRepository.Get

                    .Where(c => c.MedallionNumber.Contains(criterial.Medallion) || String.IsNullOrEmpty(criterial.Medallion)).ToList();

                listResult = (from agent in listAgent
                              join medallion in listMedallion on agent.MemberId equals medallion.MemberId into B
                              from b in B.DefaultIfEmpty()
                              select new SearchAgentResult()
                              {
                                  AccountNumber = agent.Member != null ? agent.Member.AccountNumber : string.Empty,
                                  Id = agent.Id,
                                  ChaufferLic = agent.ChaufferLic,
                                  FirstName = agent.FirstName,
                                  LastName = agent.LastName,
                                  MedallionId = b != null ? b.Id : Guid.Empty,
                                  MedallionNumber = b != null ? b.MedallionNumber : string.Empty,
                                  MemberId = agent.MemberId.HasValue?agent.MemberId.Value:Guid.Empty
                              }).Distinct().ToList();
                listResult = (from a in listResult
                              group a by a.Id into g
                              select new SearchAgentResult()
                              {
                                  AccountNumber = g != null ? g.First().AccountNumber : string.Empty,
                                   Id = g != null ? g.First(). Id : Guid.Empty,
                                  ChaufferLic = g != null ? g.First().ChaufferLic : string.Empty,
                                  FirstName = g != null ? g.First().FirstName : string.Empty,
                                  LastName = g != null ? g.First().LastName : string.Empty,
                                  MedallionId = g != null ? g.First().MedallionId : Guid.Empty,
                                  MedallionNumber = g != null ? g.First().MedallionNumber : string.Empty,
                                  MemberId = g != null ? g.First().MemberId : Guid.Empty

                              }).ToList();

            }
            return listResult;
        }
    }
}
