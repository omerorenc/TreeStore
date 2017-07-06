using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data;
using TreeStore.Data.Interface;
using TreeStore.Data.Repositories;
using TreeStore.Models;

namespace TreeStore.Services
{
   
    public interface ICampaignService
    {

        IQueryable<Campaign> GetCampaigns();
        IEnumerable<Campaign> GetCampaignsWithRelated();
        List<Campaign> GetCampaigns(string User, long id);
        Campaign GetCampaign(long id);

        void CreateCampaign(Campaign campaign);
        void UpdateCampaign(Campaign campaign);
        void DeleteCampaign(long id);
        int CountCampaign();
        void SaveCampaign();
    }
    
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository campaignRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationDbContext context;
        #region ICampaignService Members
        public CampaignService(ICampaignRepository campaignRepository, IUnitOfWork unitOfWork,ApplicationDbContext context)
        {
            this.campaignRepository = campaignRepository;
            this.unitOfWork = unitOfWork;
            this.context = context;
        }
        public int CountCampaign()
        {
            return campaignRepository.GetAll().Count();
        }

        public void CreateCampaign(Campaign campaign)
        {
            campaignRepository.Add(campaign);
        }

        public void DeleteCampaign(long id)
        {
            campaignRepository.Delete(c => c.Id == id);
        }

        public List<Campaign> GetCampaigns(string User, long id)
        {
            return campaignRepository.GetMany(c => c.CreatedBy == User && c.Id == id).ToList();
        }

        public IQueryable<Campaign> GetCampaigns()
        {
            var Campaigns = campaignRepository.GetAll();
            return Campaigns;
                 
        }

        public Campaign GetCampaign(long id)
        {
            var campaign = campaignRepository.GetById(id);
            return campaign;
        }

        public void SaveCampaign()
        {
            unitOfWork.Commit();
        }


        public void UpdateCampaign(Campaign campaign)
        {
            campaignRepository.Update(campaign);
        }

        public IEnumerable<Campaign> GetCampaignsWithRelated()
        {
            var campaigns = context.Campaigns.Include(c => c.ProductCampaign).Include(c => c.CategoryCampaign).ToList();
            return campaigns;
        }
        #endregion
    }
}
