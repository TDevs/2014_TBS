using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDevs.Domain;

 

namespace TDevs.Services
{
    public interface ICompanyService
    {
        IList<Company> Get();

        Company Get(Guid Id);

        Company Add(Company bank);

        Company Update(Company bank);

        bool Remove(Guid Id);
    }
}
