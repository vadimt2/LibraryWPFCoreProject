using Microsoft.EntityFrameworkCore;
using System;
using WpfLibraryProj.Common;

namespace WpfLibraryProj.DAL
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {

        }
        //public LibraryContext()
        //{

        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LibraryDataBase;Trusted_Connection=True;");
        }

        public virtual DbSet<AbstractItem> AbstractItems { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Journal> Journals { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<CustomerRentalModel> CustomerRentingModule { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User>().HasData(new User
        //    {
        //        Id = 1,
        //        UserName ="vadim",
        //        Password = "1234",
        //        Email = "",
        //        UserRank = UserRank.Customer
        //    });
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
