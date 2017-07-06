using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Data.Repositories;
using TreeStore.Models.Entities;

namespace TreeStore.Services
{
    public interface IAdvertisementService
    {

        IEnumerable<Advertisement> GetAdvertisements();
        List<Advertisement> GetAdvertisements(string User, long id);
        Advertisement GetAdvertisement(long id);

        void CreateAdvertisement(Advertisement advertisement);
        void UpdateAdvertisement(Advertisement advertisement);
        void DeleteAdvertisement(long id);
        int CountAdvertisement();
        void SaveAdvertisement();
    }
        public class AdvertisementService:IAdvertisementService
    {
        private readonly IAdvertisementRepository AdvertisementRepository;
        private readonly IUnitOfWork unitOfWork;


        #region IAdvertisementService Members
        public AdvertisementService(IAdvertisementRepository AdvertisementRepository, IUnitOfWork unitOfWork)
        {
            this.AdvertisementRepository = AdvertisementRepository;
            this.unitOfWork = unitOfWork;
        }
        public int CountAdvertisement()
        {
            return AdvertisementRepository.GetAll().Count();
        }

        public void CreateAdvertisement(Advertisement Advertisement)
        {
            AdvertisementRepository.Add(Advertisement);
        }

        public void DeleteAdvertisement(long id)
        {
            AdvertisementRepository.Delete(c => c.Id == id);
        }

        public List<Advertisement> GetAdvertisements(string User, long id)
        {
            return AdvertisementRepository.GetMany(c => c.CreatedBy == User && c.Id == id).ToList();
        }

        public IEnumerable<Advertisement> GetAdvertisements()
        {
            var Advertisements = AdvertisementRepository.GetAll();
            return Advertisements;

        }

        public Advertisement GetAdvertisement(long id)
        {
            var Advertisement = AdvertisementRepository.GetById(id);
            return Advertisement;
        }

        public void SaveAdvertisement()
        {
            unitOfWork.Commit();
        }


        public void UpdateAdvertisement(Advertisement Advertisement)
        {
            AdvertisementRepository.Update(Advertisement);
        }
        #endregion
    }
}
