using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class ReminderDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)MSSQLLocalDB;Database=ReminderDb;Trusted_Connection=true");
        }

        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
