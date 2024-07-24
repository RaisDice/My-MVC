using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SystemName.Models
{
    public class Genre
    {
        [Key]
        public int genreID { get; set; } //gets and sets for Genre
        public string Name { get; set; } = string.Empty;

        

    }
}

