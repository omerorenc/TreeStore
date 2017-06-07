using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TreeStore.Models.Entities;

namespace TreeStore.Models
{
    public class Category:BaseEntity
    {
        public Category()
        {
            this.ChildCategories = new HashSet<Category>();
        }
        [Display(Name="Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Kampanya Adı")]
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Category> ChildCategories { get; set; }
        public long? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public virtual ICollection<CategoryCampaign> CategoryCampaign { get; set; }

        public long? SliderId { get; set; }
        public Slider Slider { get; set; }
    }
}
