using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace UserDomain
{
    public class userContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public userContext()
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=gazo;Username=postgres;Password=gazoluck");
        }
    }
}
