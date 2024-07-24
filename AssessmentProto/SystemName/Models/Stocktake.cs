using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SystemName.Models
{
    public class Stocktake
    {
        [Key]
        public int ItemId { get; set; } //gets and sets for Genre
        public int SourceId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public double Price { get; set; } //float needs to be double, idk why

        

    }
}

