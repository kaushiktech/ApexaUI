
using ApexApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApexApi.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Advisor> Advisors { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Advisor>().HasData
                (
                    new Advisor { Id = 1, FullName = "Action",SIN=123123123},
                    new Advisor { Id = 2, FullName = "SciFi", SIN = 133123323 },
                    new Advisor { Id = 3, FullName = "History", SIN = 143123143 }
                );
        }
    }  
    
}
