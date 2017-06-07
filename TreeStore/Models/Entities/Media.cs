using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.Entities
{
    public class Media : BaseEntity
    {
        [Display(Name = "Ortam Adı")]
        public string Title { get; set; }

        [Display(Name = "Dosya Adı")]
        public string FileName { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Boyut")]
        public decimal Size { get; set; }

        [Display(Name = "Dosya Adresi")]
        public string FileUrl { get; set; }
        [Display(Name = "Dosya Tipi")]
        public string FileType { get; set; }

    }
}
