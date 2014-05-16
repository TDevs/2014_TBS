

using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using TDevs.Domain;
using TDevs.Domain.Entities;
namespace TDevs.Services
{
    public interface INotificationService
    {
        IList<Notification> Get();

        Notification Get(Guid Id);

        Notification Create(Notification notification);

        Notification Update(Notification notification);

        bool Delete(Guid Id);

        IList<Notification> GetNotifications(string userName);
    }
}
