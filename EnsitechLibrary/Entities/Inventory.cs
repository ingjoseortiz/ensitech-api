using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using server.Models;


namespace EnsitechLibrary.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        public Book? Book { get; set; }= null;
        [Required]
        [Range(1,50)]
        public int Quantity { get; set; }
        //public ICollection<Rental>? Rental { get; set; }
    }
}