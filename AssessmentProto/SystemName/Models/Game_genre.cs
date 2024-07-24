using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SystemName.Models
{
    public class Game_genre
    {
        [Key]
        public int subGenreID { get; set; } //gets and sets for Genre
        public string Name { get; set; } = string.Empty;

        

    }
}

