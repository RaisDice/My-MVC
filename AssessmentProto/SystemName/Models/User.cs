using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemName.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // an attempt at identities 
        public int UserID { get; set; } //gets and sets for Genre
        [Key]
        [Required(ErrorMessage = "Please enter a Username")]
        public string UserName { get; set; } = string.Empty;

        
        public string? Email { get; set; } = string.Empty;

        
        public string? Name { get; set; } = string.Empty;

        public bool? isAdmin { get; set; } 

       
        public string? Salt { get; set; } = string.Empty;

        public string? HashPW { get; set; } = string.Empty;

        [NotMapped]
        public string? Password { get; set; } = string.Empty;





    }
}

