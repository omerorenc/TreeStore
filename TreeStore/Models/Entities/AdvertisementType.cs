using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TreeStore.Models.Entities
{
    public enum AdvertisementType
    {
        [Display(Name = "Html")]
        Html = 1,
        [Display(Name = "Resim")]
        Image = 2
    }
}
