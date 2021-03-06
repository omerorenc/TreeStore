﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TreeStore.Models.Entities
{
    public class Slider:BaseEntity
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<Campaign> Campaigns { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
