//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Data;
//using
//
//
//
//.Domain.Entities;
//using WebApplication1.Domain.Map;
//using WebApplication1.Libs.Consumer.Service.Application.ViewModels;

//namespace WebApplication1.Context
//{
//    public class ApplicationDbContext : DbContext
//    {
//        public DbSet<Operator> Operators { get; set; }
//        public DbSet<OperatorRole> OperatorRoles { get; set; }

//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
//        {
//        }
//        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        //{
//        //    optionsBuilder.UseLazyLoadingProxies(); 
//        //}

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.ApplyConfiguration(new OperatorMap());
//            modelBuilder.ApplyConfiguration(new OperatorRolesMap());


//            base.OnModelCreating(modelBuilder);

//        }

//    }
//}
