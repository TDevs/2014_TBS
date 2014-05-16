using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDevs.Domain
{
    /// <summary>
    /// Author: KhoaHT
    /// CreatedDate: 22/12/2013
    /// </summary>
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreateDate { get; set; }
    }
}
