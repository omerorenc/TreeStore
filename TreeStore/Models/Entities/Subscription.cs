using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.Entities
{
    public class Subscription : BaseEntity
    {
        [Display(Name = "E-Posta")]
        public string Email { get; set; }
        [Display(Name = "Ad-Soyad")]
        public string FullName { get; set; }
        [Display(Name = "Abone mi?")]
        public bool IsSubscribed { get; set; }
        [Display(Name = "Abone Olma Tarihi")]
        public DateTime SubscriptionDate { get; set; }
        [Display(Name = "Abonelikten Çıkma Tarihi")]
        public DateTime UnsubscriptionDate { get; set; }
        [Display(Name = "Onaylandı mı")]
        public bool IsConfirmed { get; set; }
        [Display(Name = "Onaylanma Tarihi")]
        public DateTime ConfirmationDate { get; set; }
        [Display(Name = "Onaylanma Kodu")]
        public String ConfirmationCode { get; set; }

    }
}
