using Bases.Entities;
using Mapping;
using Microsoft.EntityFrameworkCore;

namespace Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Operator> Operators { get; set; }
        public DbSet<OperatorRole> OperatorRoles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseLazyLoadingProxies(); 
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OperatorMap());
            modelBuilder.ApplyConfiguration(new OperatorRolesMap());


            base.OnModelCreating(modelBuilder);

        }

    }
}
