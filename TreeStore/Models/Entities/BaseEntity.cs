using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models
{
    public class BaseEntity
    {
        public long Id { get; set; }
        [Display(Name = "Adı")]
        public string Name { get; set; }
        [Display(Name = "Oluşturulma Tarihi")]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Düzenlenme Tarihi")]
        public DateTime UpdateDate { get; set; }
        [Display(Name = "Oluşturan Kişi")]
        public string CreatedBy { get; set; }
        [Display(Name = "Düzenleyen Kişi")]
        public string UpdateBy { get; set; }
    }
}
