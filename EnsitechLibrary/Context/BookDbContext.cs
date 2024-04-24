using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using EnsitechLibrary.Entities;
using EnsitechLibrary.Models;
using Microsoft.EntityFrameworkCore;
using server.Models;


namespace EnsitechLibrary
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options){}
        public DbSet<Book> books{ get; set; }
        public DbSet<Rental> rentals { get; set; }
        public DbSet<Inventory> inventory { get; set; }

        protected  override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                    optionsBuilder.UseInMemoryDatabase("TestDb"); 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(e => e.Inventory)
                .WithOne(e => e.Book)
                .HasForeignKey<Inventory>(e => e.BookId)
                .IsRequired();
 
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //         modelBuilder.Entity<Book>()
        //         .HasData(
        //             new Book
        //             {
        //                 Id = 1,
        //                 Title="Atomic Habbits",
        //                 Author = "Unkown",
        //                 Genre = "Motivation"
        //             },
        //             new Book
        //             {
        //                 Id = 2,
        //                 Title="Book of Ely",
        //                 Author = "Unkown",
        //                 Genre = "Adventure"
        //             },
        //             new Book
        //             {
        //                 Id = 3,
        //                 Title="c# for Dummies",
        //                 Author = "Unkown",
        //                 Genre = "IT"
        //             },
        //             new Book
        //             {
        //                 Id = 4,
        //                 Title="Lord of the rings",
        //                 Author = "Unkown",
        //                 Genre = "Adventure"
        //             }
                    
        //         );
        // }
    }
}