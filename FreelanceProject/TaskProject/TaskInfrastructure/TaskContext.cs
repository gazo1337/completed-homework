using System.Collections.Generic;

namespace TaskInfrastructure
{
    public class TaskContext
    {
        public class UserContext : DbContext
        {
            public DbSet<TaskDomain.Task> Tasks { get; set; } = null!;
            public UserContext()
            {

            }
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=users;Username=postgres;Password=gazoluckUser");
            }
        }
}