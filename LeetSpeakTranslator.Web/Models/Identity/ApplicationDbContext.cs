using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace LeetSpeakTranslator.Web.Models.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

       // public System.Data.Entity.DbSet<LeetSpeakTranslator.Web.Models.Identity.ApplicationUser> ApplicationUsers { get; set; }
    }
}
