using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.Entities
{
    public class Advertisement:BaseEntity
    {
        [Required(ErrorMessage = "Reklam Türü alanı gereklidir.")]
        [Display(Name = "Reklam Türü")]
        [EnumDataType(typeof(AdvertisementType))]
        public AdvertisementType AdvertisementType { get; set; }
        [Required(ErrorMessage = "Reklam Konumu alanı gereklidir.")]
        [Display(Name = "Reklam Konumu")]
        [EnumDataType(typeof(AdvertisementLocation))]
        public AdvertisementLocation AdvertisementLocation { get; set; }
        [Required(ErrorMessage = "ReklamURL alanı gereklidir.")]
        [Display(Name = "ReklamURL")]
        public string AdvertisementUrl { get; set; }
        [Required(ErrorMessage = "Reklam Resmi alanı gereklidir.")]
        [Display(Name = "Reklam Resmi")]
        public string AdvertisementImage { get; set; }
        [Required(ErrorMessage = "Reklam Açıklaması alanı gereklidir.")]
        [Display(Name = "Reklam Açıklaması")]
        public string AdvertisementDescription { get; set; }
        [Required(ErrorMessage = "Reklam Aktif mi alanı gereklidir.")]
        [Display(Name = "Reklam Aktif mi?")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Reklam Başlangıç Tarihi alanı gereklidir.")]
        [Display(Name = "Reklam Başlangıç Tarihi")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Reklam Bitiş Tarihi alanı gereklidir.")]
        [Display(Name = "Reklam Bitiş Tarihi")]
        public DateTime FinishDate { get; set; }

    }
}
