using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Models.Entities;

namespace TreeStore.Models
{
    public class Campaign:BaseEntity
    {
        public Campaign()
        {
            this.CategoryCampaign = new HashSet<CategoryCampaign>();
            this.ProductCampaign = new HashSet<ProductCampaign>();
        }
        [Display(Name=("Kampanya Açıklaması"))]
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
        [NotMapped]
        public long[] ProductIds { get; set; }
        [NotMapped]
        public long[] CategoryIds { get; set; }
        public virtual ICollection<ProductCampaign> ProductCampaign { get; set; }
        public virtual ICollection<CategoryCampaign> CategoryCampaign { get; set; }

        public long? SliderId { get; set; }
        public Slider Slider { get; set; }
    }
}
