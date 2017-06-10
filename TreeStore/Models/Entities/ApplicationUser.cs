using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TreeStore.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Categories = new HashSet<Category>();
            Products = new HashSet<Product>();
            Campaigns = new HashSet<Campaign>();
            this.EmailConfirmed = false;
        }
        public string CompanyName { get; set; }
        public virtual ICollection<Category>Categories{ get; set; }
        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public DateTime UpdatedDate { get; set; }
        
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Logo { get; set; }


    }
}
