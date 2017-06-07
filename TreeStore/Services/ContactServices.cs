using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Data.Interface;
using TreeStore.Data.Repositories;
using TreeStore.Models.Entities;

namespace TreeStore.Services
{
    public interface IContactService
    {

        IEnumerable<Contact> GetContacts();

        List<Contact> GetContacts(string User, long id);
        Contact GetContact(long id);
        void CreateContact(Contact Contact);
        void UpdateContact(Contact Contact);
        void DeleteContact(long id);
        int CountContact();
        void SaveContact();
    }

    public class ContactService : IContactService
    {
        private readonly IContactRepository ContactRepository;
        private readonly IUnitOfWork unitOfWork;

        #region IContactService Members
        public ContactService(IContactRepository ContactRepository, IUnitOfWork unitOfWork)
        {
            this.ContactRepository = ContactRepository;
            this.unitOfWork = unitOfWork;
        }
        public int CountContact()
        {
            return ContactRepository.GetAll().Count();
        }

        public void CreateContact(Contact Contact)
        {
            ContactRepository.Add(Contact);
        }

        public void DeleteContact(long id)
        {
            ContactRepository.Delete(c => c.Id == id);
        }

        public List<Contact> GetContacts(string User, long id)
        {
            return ContactRepository.GetMany(c => c.CreatedBy == User && c.Id == id).ToList();
        }

        public IEnumerable<Contact> GetContacts()
        {
            var Contacts = ContactRepository.GetAll();
            return Contacts;

        }

        public Contact GetContact(long id)
        {
            var Contact = ContactRepository.GetById(id);
            return Contact;
        }

        public void SaveContact()
        {
            unitOfWork.Commit();
        }


        public void UpdateContact(Contact Contact)
        {
            ContactRepository.Update(Contact);
        }
        #endregion
    }
}
