
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TDevs.Core.Constants;
using TDevs.Core.Infrastructure;
using TDevs.Domain;
using TDevs.Domain.Entities;
using TDevs.Infrastructure;


namespace TDevs.Services
{
    [ExcludeFromCodeCoverage]
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> notificationRepository;
        public NotificationService(IRepository<Notification> notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        IList<Notification> INotificationService.Get()
        {
            throw new NotImplementedException();
        }

        public Notification Get(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Notification Create(Notification notification)
        {
            throw new NotImplementedException();
        }

        public Notification Update(Notification notification)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public IList<Notification> GetNotifications(string userName)
        {
            var list = notificationRepository.Get.Where(n => n.CreateBy.Equals(userName)).ToList();

            return list ?? new List<Notification>();
        }
    }
}