using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EnsitechLibrary.Entities;
using EnsitechLibrary.Models;
using EnsitechLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using server.Helpers;

namespace EnsitechLibrary.Controllers
{
    [ApiController, Route("api/books")] 
    public class BooksController : ControllerBase
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookRepository _repository;
 
        public BooksController(ILogger<BooksController> logger, IBookRepository repository)
        {
            _logger = logger;
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));     
        }
    
        [AllowAnonymous]
        [HttpGet("all")]
        public  IActionResult GetBooks()
        {
            var books = _repository.GetBooks();
 
            Console.WriteLine(books);
 
            return  Ok(books); //200
        }
 
        [Authorize(Roles="administrator")]
        [HttpPost("add")]
        public IActionResult AddBooks([FromBody] BookDto bookDto)
        {             
            string guid = Guid.NewGuid().ToString();
            Random rnd = new Random();
            int num = rnd.Next();
            var books =
                new Book
                {
                    Id = num,//corregir, require to be automatic
                    Title= bookDto.Title,
                    Author = bookDto.Author,
                    Genre = bookDto.Genre,
                    Inventory = new Inventory
                    {
                        Id= num, 
                        BookId= num,
                        Quantity = bookDto.InventoryDto.Quantity,
                    }
                };
            var booksJson = JsonConvert.SerializeObject(books);
            _logger.LogInformation($"BookController _Ilooger Book Created {booksJson}");
            Console.WriteLine(books); //remove
            _repository.CreateBook(books);
            return Ok(books);
        }

        [Authorize(Roles="administrator")]
        [HttpPost("remove")]
        public IActionResult RemoveBooks([FromBody] BookDto bookDto)
        {             
            // we only need ID
            var books =
                    new Book
                    {
                        Id = bookDto.Id,
                        Title= bookDto.Title,
                        Author = bookDto.Author,
                        Genre = bookDto.Genre
                    };
                    
            Console.WriteLine("book for delete:" + bookDto.Id);
            _repository.DeleteBook(bookDto.Id);
            return Ok(books);
        }

        [Authorize(Roles="administrator")]
        [HttpPatch("update")]
        public IActionResult Update([FromBody] BookDto bookDto)//param ID?
        {
            System.Console.WriteLine("bookDto fromBody" + bookDto);
        var existingBook = _repository.GetBook(bookDto.Id);
        if (existingBook == null)
        {
            return NotFound();
        }
        existingBook.Inventory.Quantity = bookDto.InventoryDto.Quantity;
 
            _repository.PatchBook(existingBook);
            return Ok(existingBook);
        }

        [Authorize(Roles="client, administrator")]
        [HttpPatch("rent")]
        public IActionResult Rent([FromBody] BookDto bookDto)
        {
            var existingBook = _repository.GetBook(bookDto.Id);
            if (existingBook == null)
            {
                return NotFound();
            } 
            int userId = Convert.ToInt16(HttpContext.User.FindFirstValue("id"));
            _repository.RentBook(existingBook.Id, userId);
            return Ok(existingBook);
        }

        [Authorize(Roles="client, administrator")]
        [HttpPatch("return")]
        public IActionResult Return([FromBody] BookDto bookDto)
        {
            var existingBook = _repository.GetBook(bookDto.Id);
            //System.Console.WriteLine("existingBook",existingBook.Id);
            if (existingBook == null)
            {
                return NotFound();
            } 
            _repository.Return(existingBook.Id);
            return Ok(existingBook);
        }

        [AllowAnonymous]
        [HttpPost("rented")]
        public string Rented()
        { 
            var booksRented = _repository.Rented();
            return booksRented;        
        }


    }
}