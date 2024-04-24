using System;

using EnsitechLibrary.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using server.Models; 
using Newtonsoft.Json;
using System.Text.Json.Nodes;

namespace EnsitechLibrary.Services
{
    public class BookRepository : IBookRepository
    {
        private BookDbContext _context;
      
        public BookRepository(BookDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
                        
            Random r = new Random();
            int n = r.Next(0, 10);

            var books = new List<Book>
            { 
                    new Book
                    {
                        Id = n,
                        Title="Atomic Habbits",
                        Author = "Unkown",
                        Genre = "Motivation"
                    },
                    // new Book
                    // {
                    //     Id = 2,
                    //     Title="Book of Ely",
                    //     Author = "Unkown",
                    //     Genre = "Adventure"
                    // },
                    // new Book
                    // {
                    //     Id = 3,
                    //     Title="c# for Dummies",
                    //     Author = "Unkown",
                    //     Genre = "IT"
                    // },
                    // new Book
                    // {
                    //     Id = 4,
                    //     Title="Lord of the rings",
                    //     Author = "Unkown",
                    //     Genre = "Adventure"
                    // }
            };
            if (books.Count == 0){

            _context.books.AddRange(books);
            _context.SaveChanges();
            //_context.Dispose();
            }
        }

        public void CreateBook(Book book)
        {
            _context.books.Add(book);
            _context.SaveChanges(); 
        }

        public void DeleteBook(int bookId)
        {
           var selectedBook =  _context.books.Find(bookId);
           //System.Console.WriteLine("selectedbook " + selectedBook.Id);
           if(selectedBook != null){ 
            _context.books.Remove(selectedBook);
            _context.SaveChanges(); 
           // System.Console.WriteLine("Book deleted" + selectedBook.Id);
           }
        }

        public Book GetBook(int id)
        { 
            return _context.books.Include(i => i.Inventory)
            .FirstOrDefault(b => b.Id == id) ?? throw new ArgumentNullException(nameof(id));  
        }

        public IEnumerable<Book> GetBooks()
        {
           var books = _context.books.Include(i=> i.Inventory).ToList();   
           return books;
        }

        public void PatchBook(Book book)
        {
            _context.books.Update(book);
            _context.SaveChanges(true);
            _context.Dispose();
        }

        public void RentBook(int bookId, int userId)
        {
            var selectedBook =  _context.books.Find(bookId);
            var IsAnotherActiveRented = _context.rentals
                .FirstOrDefault(r => 
                r.BookId == bookId && 
                r.IsReturned ==false 
                && r.UserId == userId
            );

            if (IsAnotherActiveRented != null) return;

            if (selectedBook.Inventory.Quantity == 0){
                System.Console.WriteLine("No books available for rent");
                return;
            } 
      
            System.Console.WriteLine("selected book to rent", selectedBook.Id);
            if(selectedBook != null)
            { 
                selectedBook.Inventory.Quantity = selectedBook.Inventory.Quantity - 1;
                _context.books.Update(selectedBook); 
                _context.SaveChanges();

                var rent = new Rental {
                    Id = Guid.NewGuid(),
                    BookId = selectedBook.Id,
                    UserId = userId,
                    Date = DateTime.Now,
                    IsActive = true, 
                };

                _context.rentals.Add(rent);
                _context.SaveChanges();
 
                _context.Dispose();

                System.Console.WriteLine("Book Rented: " + selectedBook.Id);
                System.Console.WriteLine("Book Inventory Remains" + selectedBook.Inventory.Quantity);
            }
        }

   
        public string Rented()
        { 
            var list = from b in _context.books                             
            join r in _context.rentals on b.Id equals r.BookId into BooksRental                         
            from rb in BooksRental.DefaultIfEmpty()           
            where  rb.IsActive != null
            
            select new 
            {
                b.Id,
                b.Title,
                b.Author,
                b.Genre,
                UserId = rb.UserId ==  0 ? rb.UserId : 0, 
                BookId = rb.BookId  ==  0 ? rb.UserId : 0,
                rb.IsActive,
                rb.IsReturned,
                b.Inventory.Quantity,
                Date = rb.Date.ToString("yyyy-MM-dd")
            };

            var json = JsonConvert.SerializeObject(list);
            System.Console.WriteLine(json);  
            return json;        
        }

        public void Return(int bookId)
        {
            var selectedBook =  _context.books.Find(bookId);

            if (selectedBook is null){
                System.Console.WriteLine("No books found"); 
                return;
            }

            if(selectedBook != null){ 
                System.Console.WriteLine("selected book to return: " + selectedBook.Id);
                selectedBook.Inventory.Quantity = selectedBook.Inventory.Quantity + 1;

                var returnBook = _context.rentals.FirstOrDefault(r =>r.BookId == bookId && r.IsReturned==false);
                returnBook.IsActive = false;
                returnBook.IsReturned = true;

                _context.rentals.Update(returnBook);
                _context.books.Update(selectedBook); 

                    // _rental.RentBook(selectedBook.Id, );
                    _context.SaveChanges();
                    _context.Dispose();
                    System.Console.WriteLine("Book returned" + selectedBook.Id);
                    System.Console.WriteLine("Book Inventory Remains" + selectedBook.Inventory.Quantity);
            }
       
        }
    }
}