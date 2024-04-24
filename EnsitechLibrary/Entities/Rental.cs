using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnsitechLibrary.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.Models
{
    public class Rental
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int BookId { get; set; }
        public bool? IsActive { get; set; } = false;
        public bool? IsReturned { get; set; } = false;
        public Book Book { get; set; }
 
    }
}