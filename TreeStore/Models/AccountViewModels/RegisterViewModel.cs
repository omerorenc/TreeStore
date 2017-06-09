using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} en az {2} en fazla {1} karakter uzunluğunda olabilir.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre(Tekrar)")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor. Lütfen tekrar deneyin.")]
        public string ConfirmPassword { get; set; }
        [Required]
        [Display(Name ="Firma Adı")]
        public string CompanyName { get; set; }
        [Display(Name ="Firma Adresi")]
        public string Address { get; set; }
        [Required(ErrorMessage ="Lütfen firma telefonunu giriniz.")]
        [Display(Name = "Firma Telefonu")]
        public string Phone { get; set; }
        [Display(Name = "Firma Fax")]
        public string Fax { get; set; }
        [Display(Name = "Logo")]
        public string Logo { get; set; }
    }
}
