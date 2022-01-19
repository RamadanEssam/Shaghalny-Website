using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Jop_Offers_Website.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; set; }

        //عشان الربط اليوزر الى رفع الوظيفه بالوظيفه الى رفعها 
        public virtual ICollection<Job> Jobs { get; set; }

        //----------------------------------
        public   string About { get; set; }
        public  string image { get; set; }
        public  string YearOfBirth { get; set; }
        public  string Governorate { get; set; }
        //--------------------------------
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.Job> Jobs { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.ApplyForJob> ApplyForJobs { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.MessageModel> MessageModels { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.Governorates> Governorates { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.Gender> Genders { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.ExperienceLevel> ExperienceLevels { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.Qualification> Qualifications { get; set; }

        public System.Data.Entity.DbSet<Jop_Offers_Website.Models.SavedJobs> SavedJobs { get; set; }

    }
}