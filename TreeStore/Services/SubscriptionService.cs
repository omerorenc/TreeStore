using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Data.Repositories;
using TreeStore.Models.Entities;

namespace TreeStore.Services
{
    public interface ISubscriptionService
    {

        IEnumerable<Subscription> GetSubscriptions();
        List<Subscription> GetSubscriptions(string Email, long id);
        Subscription GetSubscription(long id);
        void CreateSubscription(Subscription Subscription);
        void UpdateSubscription(Subscription Subscription);
        void DeleteSubscription(long id);
        int CountSubscription();
        void SaveSubscription();
    }

    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository SubscriptionRepository;
        private readonly IUnitOfWork unitOfWork;

        #region IProductService Members
        public SubscriptionService(ISubscriptionRepository SubscriptionRepository, IUnitOfWork unitOfWork)
        {
            this.SubscriptionRepository = SubscriptionRepository;
            this.unitOfWork = unitOfWork;
        }
        public int CountSubscription()
        {
            return SubscriptionRepository.GetAll().Count();
        }

        public void CreateSubscription(Subscription Subscription)
        {
            SubscriptionRepository.Add(Subscription);
        }

        public void DeleteSubscription(long id)
        {
            SubscriptionRepository.Delete(c => c.Id == id);
        }

        public List<Subscription> GetSubscriptions(string Email, long id)
        {
            return SubscriptionRepository.GetMany(c => c.Email == Email && c.Id == id).ToList();
        }

        public IEnumerable<Subscription> GetSubscriptions()
        {
            var Subscriptions = SubscriptionRepository.GetAll();
            return Subscriptions;

        }

        public Subscription GetSubscription(long id)
        {
            var Subscription = SubscriptionRepository.GetById(id);
            return Subscription;
        }

        public void SaveSubscription()
        {
            unitOfWork.Commit();
        }


        public void UpdateSubscription(Subscription Subscription)
        {
            SubscriptionRepository.Update(Subscription);
        }
        #endregion
    }
}
