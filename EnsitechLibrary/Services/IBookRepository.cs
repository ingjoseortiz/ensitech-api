using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using EnsitechLibrary.Controllers;
using EnsitechLibrary.Entities;
using EnsitechLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnsitechLibrary.Services
{
    public interface IBookRepository
    {
        //string GetBooks(); 
        IEnumerable<Book> GetBooks(); 
        Book GetBook(int id);

        void CreateBook(Book book);
        void DeleteBook(int  bookId);
        void PatchBook(Book book);
        void RentBook(int bookId, int userId); 
        void Return(int bookId); 
        string Rented();
        
    }
}