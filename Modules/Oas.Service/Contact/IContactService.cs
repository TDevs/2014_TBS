using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

namespace TDevs.Services
{
    public interface IContactService
    {
        IList<Contact> Get();

        Contact Get(Guid Id);

        Contact Add(Contact contact);

        Contact Update(Contact contact);

        bool Remove(Guid Id);
    }
}
