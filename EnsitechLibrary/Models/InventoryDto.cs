using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnsitechLibrary.Models
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public BookDto? BookDto { get; set; } = null;
        public int Quantity { get; set; }
    }
}