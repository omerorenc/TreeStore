using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.Entities
{
    public enum AdvertisementLocation
    {
        [Display(Name = "SliderAlt1")]
        Underslider1 = 1,
        [Display(Name = "SliderAlt2")]
        Underslider2= 2,
        [Display(Name = "KategoriAltı")]
        Undercategory = 3,
        [Display(Name = "ÜrünlerUstu")]
        Onproducts = 4,
        [Display(Name = "KampanyalarUstu")]
        Oncampaigns = 4
    }
}
