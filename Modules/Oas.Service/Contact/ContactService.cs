using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Core.Infrastructure;
using TDevs.Domain;

namespace TDevs.Services
{
    public class ContactService : IContactService
    {
        private readonly IRepository<Contact> contactRepository;
        public ContactService(IRepository<Contact> contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public IList<Contact> Get()
        {
            return contactRepository.Get.ToList();
        }

        public Contact Get(Guid Id)
        {
            return contactRepository.Get.FirstOrDefault(c => c.Id == Id);
        }

        public Contact Add(Contact contact)
        {
            contactRepository.Add(contact);
            return contact;
        }

        public Contact Update(Contact contact)
        {
            contactRepository.Update(contact);
            return contact;
        }

        public bool Remove(Guid Id)
        {
            var contact = Get(Id);
            if(contact==null)return false;
            contactRepository.Remove(contact);
            return true;
        }
    }
}
