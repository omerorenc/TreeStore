using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeStore.Data;
using TreeStore.Models.Entities;
using MimeKit;
using MailKit.Net.Smtp;
using TreeStore.Services;

namespace TreeStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactsController : Controller
    {
        private readonly IContactService contactService;
        private readonly IMailSettingService mailSettingService;
        public ContactsController(IContactService _contactService, IMailSettingService _mailSettingService)
        {
            this.contactService = _contactService;
            this.mailSettingService = _mailSettingService;
        }

        // GET: Admin/Contacts
        public IActionResult Index()
        {
            var contact = contactService.GetContacts();
            return View(contact);
        }

        // GET: Admin/Contacts/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contact = contactService.GetContacts().AsQueryable()
                .SingleOrDefault(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // GET: Admin/Contacts/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = contactService.GetContacts().AsQueryable()
                .SingleOrDefault(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Admin/Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long? id)
        {
            var contact = contactService.GetContacts().AsQueryable().SingleOrDefault(m => m.Id == id);
            contactService.DeleteContact(contact.Id);
            contactService.SaveContact();
            return RedirectToAction("Index");
        }
        public IActionResult Reply(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = contactService.GetContacts().AsQueryable().SingleOrDefault(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reply(long? id, Contact contact)
        {
            MailSetting mailSetting = mailSettingService.GetMailSettings().FirstOrDefault();
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    contactService.GetContacts().Where(c => c.Id == id).FirstOrDefault().Reply = contact.Reply;
                    Methods.SendMail(mailSetting, contact);
                    contactService.SaveContact();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists((long)contact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }
            return View(contact);
        }

        private bool ContactExists(long? id)
        {
            return contactService.GetContacts().Any(c => c.Id == id);
        }

    }
}