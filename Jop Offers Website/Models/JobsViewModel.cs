using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jop_Offers_Website.Models
{
    public class JobsViewModel
    {
        public string JobTitel { get; set; }
        public IEnumerable<ApplyForJob> Items { get; set; }
    }
}