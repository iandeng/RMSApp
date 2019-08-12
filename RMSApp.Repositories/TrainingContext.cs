using Microsoft.EntityFrameworkCore;
using RMSApp.Contracts;
using RMSApp.Entities.Models;
using System;

namespace RMSApp.Repositories
{
    public class TrainingContext : DbContext
    {
        public TrainingContext(DbContextOptions<TrainingContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Training> Trainings { get; set; }
    }
}
