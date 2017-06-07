using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.ViewComponents
{
    public class CampaignViewModel
    {
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Düzenlenme Tarihi")]
        public DateTime UpdateDate { get; set; }
        [Display(Name = ("Kampanya Açıklaması"))]
        public string Description { get; set; }
        [Display(Name = ("Slogan"))]
        public string Slogan { get; set; }
        [Display(Name = ("Kampanya Başlangıç Tarihi"))]
        public DateTime StartedDate { get; set; }
        [Display(Name = ("Kampanya Bitiş Tarihi"))]
        public DateTime EndDate { get; set; }
        [Display(Name = ("Kampanya resmi"))]
        public string ImagePath { get; set; }
        [Display(Name = ("Kampanya aktif mi?"))]
        public bool IsActive { get; set; }
        public virtual ICollection<ProductCampaign> ProductCampaign { get; set; }
        public virtual ICollection<CategoryCampaign> CategoryCampaign { get; set; }
    }
}
