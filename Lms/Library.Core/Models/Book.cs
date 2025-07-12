using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(150, ErrorMessage = "Title cannot be longer than 150 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, ErrorMessage = "Author name cannot be longer than 100 characters.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(20, ErrorMessage = "ISBN cannot be longer than 20 characters.")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Total copies are required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Total copies must be 0 or greater.")]
        public int TotalCopies { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(100, ErrorMessage = "Category cannot be longer than 100 characters.")]
        public string Category { get; set; }


        [Required(ErrorMessage = "Available copies are required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Available copies must be 0 or greater.")]

        public int AvailableCopies { get; set; }
    }
}
