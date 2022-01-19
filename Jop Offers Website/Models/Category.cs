using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Jop_Offers_Website.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Display (Name = "نوع الوظيفه")]
        public string CatogryName { get; set; }
        
        [Display(Name = "وصف النوع")]
        public string CatogryDescription { get; set; }
        public virtual ICollection<Job> Jobs { get; set; }
    }
}