using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.ViewComponents
{
    public class ContactViewModel
    {
        [Display(Name = "Ad Soyad")]
        public string FullName { get; set; }
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Mesaj")]
        public string Message { get; set; }
    }
}
