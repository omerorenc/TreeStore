using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} en az {2} en fazla {1} karakter uzunluğunda olmalıdır!!!", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name ="Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre(Tekrar)")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor!!")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
