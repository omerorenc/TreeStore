using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.ViewComponents
{
    public class CategoryViewModel
    {
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Düzenlenme Tarihi")]
        public DateTime UpdateDate { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Kampanya Adı")]
        public long? CampaignId { get; set; }
        public virtual Campaign Campaign { get; set; }
        [Display(Name = "Ürün Adı")]
        public long? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<CategoryCampaign> CategoryCampaign { get; set; }
    }
}
