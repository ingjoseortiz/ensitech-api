using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using EnsitechLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models;

namespace server.Services
{ 
    public class RentalRepository
    {
        private readonly ILogger<RentalRepository> _logger;
        private BookDbContext _context;
        public RentalRepository(ILogger<RentalRepository> logger)
        {
            _logger = logger ?? null;
        }

        public void RecoverAllBooks(int bookId, int userId)
        {
            throw new NotImplementedException();
        }

        public void RentBook(int bookId, int userId)
        {
           var newRental = new Rental {
             Id = Guid.NewGuid(),
              BookId= bookId,
              UserId= userId,
              IsActive = true,
              Date = DateTime.Now    
           };
           _context.rentals.Add(newRental);
        }

        public void ReturnBook(int bookId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}