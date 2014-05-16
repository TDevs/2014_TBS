using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;



namespace TDevs.Services
{

    public interface ISupportService
    {
        IList<FAQ> GetFAQs();
        FAQ GetFAQ(Guid Id);

        FAQ AddFAQ(FAQ faq);


        FAQ UpdateFAQ(FAQ faq);

        bool RemoveFAQ(Guid Id);



        IList<Tutorial> GetTutorials();
        Tutorial GetTutorial(Guid Id);

        Tutorial AddTutorial(Tutorial faq);


        Tutorial UpdateTutorial(Tutorial faq);

        bool RemoveTutorial(Guid Id);


        IList<Ticket> GetSupportTickets();

        Ticket GetSupportTicket(Guid Id);

        Ticket AddSupportTicket(Ticket supportTicket);

        Ticket UpdateSupportTicket(Ticket supportTicket);

        Ticket ReplySupportTicket(Ticket supportTicket);

        bool RemoveSupportTicket(Guid Id);
    }

}
