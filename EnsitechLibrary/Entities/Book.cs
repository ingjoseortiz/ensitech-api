using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EnsitechLibrary.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [StringLength(20)]
        [Required]
        public string Title { get; set; } = "";
        [StringLength(20)]
        [Required]
        public string Author { get; set; }= "";
        [StringLength(20)]
        [Required]
        public string Genre { get; set; } = "";
        public Inventory? Inventory { get; set; }
    }
}