using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class SupportService : ISupportService
    {
        private readonly IRepository<FAQ> faqRepository;
        private readonly IRepository<Tutorial> tutorialRepository;
        private readonly IRepository<Ticket> ticketRepository;
        private readonly IRepository<TicketItem> ticketItemRepository;
        private readonly IRepository<Notification> notificationRepository;

        public SupportService(IRepository<FAQ> faqRepository,
            IRepository<Tutorial> tutorialRepository,
            IRepository<Ticket> ticketRepository,
            IRepository<Notification> notificationRepository,
            IRepository<TicketItem> ticketItemRepository)
        {
            this.faqRepository = faqRepository;
            this.tutorialRepository = tutorialRepository;
            this.ticketRepository = ticketRepository;
            this.ticketItemRepository = ticketItemRepository;
            this.notificationRepository = notificationRepository;
        }

        public IList<FAQ> GetFAQs()
        {
            return faqRepository.Get.ToList();
        }

        public FAQ GetFAQ(Guid Id)
        {
            return faqRepository.Get.FirstOrDefault(t => t.Id.Equals(Id));
        }

        public FAQ AddFAQ(FAQ faq)
        {
            faq.Id = Guid.NewGuid();
            var obj = faqRepository.Add(faq);
            return obj;
        }

        public FAQ UpdateFAQ(FAQ faq)
        {
            var obj = faqRepository.Update(faq);
            return obj;
        }

        public bool RemoveFAQ(Guid Id)
        {
            var obj = faqRepository.Get.FirstOrDefault(t => t.Id.Equals(Id));
            if (obj != null)
            {
                faqRepository.Remove(obj);
                return true;
            }
            return false;
        }

        public IList<Tutorial> GetTutorials()
        {
            return tutorialRepository.Get.ToList();
        }

        public Tutorial GetTutorial(Guid Id)
        {
            return tutorialRepository.Get.FirstOrDefault(t => t.Id.Equals(Id));
        }

        public Tutorial AddTutorial(Tutorial faq)
        {
            faq.Id = Guid.NewGuid();
            var obj = tutorialRepository.Add(faq);
            return obj;
        }

        public Tutorial UpdateTutorial(Tutorial faq)
        {
            var obj = tutorialRepository.Update(faq);
            return obj;
        }

        public bool RemoveTutorial(Guid Id)
        {
            var obj = tutorialRepository.Get.FirstOrDefault(t => t.Id.Equals(Id));
            if (obj != null)
            {
                tutorialRepository.Remove(obj);
                return true;
            }
            return false;
        }

        public IList<Ticket> GetSupportTickets()
        {
            return ticketRepository.Get.ToList();
        }

        public Ticket GetSupportTicket(Guid Id)
        {
            return ticketRepository.Get.FirstOrDefault(t => t.Id.Equals(Id));
        }

        public Ticket AddSupportTicket(Ticket supportTicket)
        {

            var obj = ticketRepository.Add(supportTicket);

            var noti = new Notification()
            {
                Id = Guid.NewGuid(),
                To = "admin",
                CreateBy = supportTicket.CreateBy,
                CreateAt = DateTime.Now,
                Read = false,
                Title = supportTicket.Title

            };

            notificationRepository.Add(noti);

            return obj;
        }

        public Ticket UpdateSupportTicket(Ticket supportTicket)
        {
            var obj = ticketRepository.Update(supportTicket);

            return obj;
        }

        public bool RemoveSupportTicket(Guid Id)
        {
            var obj = ticketRepository.Get.FirstOrDefault(t => t.Id.Equals(Id));
            if (obj != null)
            {
                ticketRepository.Remove(obj);
                return true;
            }
            return false;
        }


        public Ticket ReplySupportTicket(Ticket supportTicket)
        {
            foreach (var item in supportTicket.TicketItems)
            {
                if (item.Id == Guid.Empty)
                {
                    item.Id = Guid.NewGuid();
                    ticketItemRepository.Add(item);
                }
            }

            //Notify to ticket owner
            var noti = new Notification()
            {
                Id = Guid.NewGuid(),
                To = supportTicket.CreateBy,
                CreateBy = supportTicket.CreateBy,
                CreateAt = DateTime.Now,
                Read = false,
                Title = supportTicket.Title

            };

            notificationRepository.Add(noti);
            return supportTicket;
        }
    }
}
