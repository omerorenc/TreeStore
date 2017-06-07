using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data;
using TreeStore.Data.Interface;
using TreeStore.Data.Repositories;
using TreeStore.Models.Entities;

namespace TreeStore.Services
{
    public interface ISettingService
    {

        IEnumerable<Setting> GetSettings();

        List<Setting> GetSettings(string User, long id);
        Setting GetSetting(long id);
        void CreateSetting(Setting Setting);
        void UpdateSetting(Setting Setting);
        void DeleteSetting(long id);
        int CountSetting();
        void SaveSetting();
    }

    public class SettingService : ISettingService
    {
        private readonly ISettingRepository SettingRepository;
        private readonly IUnitOfWork unitOfWork;

        #region ISettingService Members
        public SettingService(ISettingRepository SettingRepository, IUnitOfWork unitOfWork)
        {
            this.SettingRepository = SettingRepository;
            this.unitOfWork = unitOfWork;
        }
        public int CountSetting()
        {
            return SettingRepository.GetAll().Count();
        }

        public void CreateSetting(Setting Setting)
        {
            SettingRepository.Add(Setting);
        }

        public void DeleteSetting(long id)
        {
            SettingRepository.Delete(c => c.Id == id);
        }

        public List<Setting> GetSettings(string User, long id)
        {
            return SettingRepository.GetMany(c => c.CreatedBy == User && c.Id == id).ToList();
        }

        public IEnumerable<Setting> GetSettings()
        {
            var Settings = SettingRepository.GetAll();
            return Settings;

        }

        public Setting GetSetting(long id)
        {
            var Setting = SettingRepository.GetById(id);
            return Setting;
        }

        public void SaveSetting()
        {
            unitOfWork.Commit();
        }


        public void UpdateSetting(Setting Setting)
        {
            SettingRepository.Update(Setting);
        }
        #endregion
    }
}