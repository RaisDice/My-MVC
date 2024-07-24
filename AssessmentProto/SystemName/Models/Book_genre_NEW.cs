using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemName.Models
{
    [Table("Book_genre NEW")]
    public class Book_genre_NEW
    {
        [Key]
        public int subGenreID { get; set; } //gets and sets for Genre
        public string Name { get; set; } = string.Empty;

        
    }
}

