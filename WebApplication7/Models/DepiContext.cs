using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication7.Models
{
    public class DepiContext : IdentityDbContext<ApplicationUser>
    {

        public DepiContext() : base()
        {

        }
        public DepiContext(DbContextOptions options) : base(options)
        { }

			public DbSet<User> User { get; set; }
		    public DbSet<Admin> Admin { get; set; }
	
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=LAPTOP-ASFDTF01\\MSSQLSERVERR;Database=Depi;Trusted_Connection=True;Encrypt=False;");
            //base.OnConfiguring(optionsBuilder);
        }

    }
}

