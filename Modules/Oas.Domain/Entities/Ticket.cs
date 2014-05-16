using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDevs.Domain
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; }


        public string Title { get; set; }       

        public string CreateBy { get; set; }

        public string Status { get; set; }

        public virtual ICollection<TicketItem> TicketItems { get; set; }

    }

    public class TicketItem
    {
        public Guid Id { get; set; }

        public Guid SupportTicketId { get; set; }

        public string Message { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateAt { get; set; }

        [ForeignKey("SupportTicketId")]
        public virtual Ticket SupportTicket { get; set; }

        public bool Read { get; set; }
    }

    public class Notification
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string To { get; set; }

        public DateTime CreateAt { get; set; }

        public string CreateBy { get; set; }

        public bool Read { get; set; }
    }
}
