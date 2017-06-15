using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.AccountViewModels
{
    public class LoginViewModel : BaseEntity
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre(Tekrar)")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor. Lütfen tekrar deneyin.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        [Required]
        [Display(Name = "Firma Adı")]
        public string CompanyName { get; set; }

        [Display(Name = "Firma Adresi")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Lütfen firma telefonunu giriniz.")]
        [Display(Name = "Firma Telefonu")]
        public string Phone { get; set; }

        [Display(Name = "Firma Fax")]
        public string Fax { get; set; }
    }
        }
       

