using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.ViewComponents
{
    public class ProductViewModel
    {
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Düzenlenme Tarihi")]
        public DateTime UpdateDate { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Ücret")]
        public decimal Price { get; set; }

        [Display(Name = "İndirimli Fiyat")]
        public decimal DiscountPrice { get; set; }

        [Display(Name = "Resim Kaynağı")]
        public string ImagePath { get; set; }

        [Display(Name = "Aktif mi?")]
        public bool IsActive { get; set; }

        [Display(Name = "Kampanya Linki")]
        public string CompanyLink { get; set; }
        public string Code { get; set; }
        public virtual ICollection<ProductCampaign> ProductCampaign { get; set; }
    }
}
