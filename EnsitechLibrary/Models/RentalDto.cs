using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnsitechLibrary.Models;

namespace server.Models
{
    public class RentalDto
    {
       public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int BookId { get; set; }
        public bool? IsActive { get; set; } = false;
        public bool? IsReturned { get; set; } = false;
    }
}