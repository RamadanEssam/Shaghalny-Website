using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jop_Offers_Website.Models
{
    public class SavedJobs
    {
        public int id { get; set; }
        public string userId { get; set; }
        public int jobId { get; set; }
        public Job job { get; set; }
        public ApplicationUser user { get; set; }
    }
}