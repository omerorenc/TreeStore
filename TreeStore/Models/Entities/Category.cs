using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models
{
    public class Category:BaseEntity
    {
        [Display(Name="Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Kampanya Adı")]
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<CategoryCampaign> CategoryCampaign { get; set; }
    }
}
