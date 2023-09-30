
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sample.WebApi.Data.Models;

namespace Sample.WebApi.Data
{
   public class AppDbContext :IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
       public DbSet<Brand> Brands { get; set; }
    }
}
