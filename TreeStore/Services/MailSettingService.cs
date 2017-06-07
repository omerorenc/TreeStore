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
    public interface IMailSettingService
    {

        IEnumerable<MailSetting> GetMailSettings();

        List<MailSetting> GetMailSettings(string User, long id);
        MailSetting GetMailSetting(long id);
        void CreateMailSetting(MailSetting MailSetting);
        void UpdateMailSetting(MailSetting MailSetting);
        void DeleteMailSetting(long id);
        int CountMailSetting();
        void SaveMailSetting();
    }

    public class MailSettingService : IMailSettingService
    {
        private readonly IMailSettingRepository MailSettingRepository;
        private readonly IUnitOfWork unitOfWork;

        #region IMailSettingService Members
        public MailSettingService(IMailSettingRepository MailSettingRepository, IUnitOfWork unitOfWork)
        {
            this.MailSettingRepository = MailSettingRepository;
            this.unitOfWork = unitOfWork;
        }
        public int CountMailSetting()
        {
            return MailSettingRepository.GetAll().Count();
        }

        public void CreateMailSetting(MailSetting MailSetting)
        {
            MailSettingRepository.Add(MailSetting);
        }

        public void DeleteMailSetting(long id)
        {
            MailSettingRepository.Delete(ms => ms.Id == id);
        }

        public MailSetting GetMailSetting(long id)
        {
            var MailSetting = MailSettingRepository.GetById(id);
            return MailSetting;
        }

        public IEnumerable<MailSetting> GetMailSettings()
        {
            var MailSettings = MailSettingRepository.GetAll();
            return MailSettings;
        }

        public List<MailSetting> GetMailSettings(string User, long id)
        {
            return MailSettingRepository.GetMany(c => c.CreatedBy == User && c.Id == id).ToList();
        }

        public void SaveMailSetting()
        {
            unitOfWork.Commit();
        }

        public void UpdateMailSetting(MailSetting MailSetting)
        {
            MailSettingRepository.Update(MailSetting);
        }
        #endregion
    }
}
