using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SystemName.Models;

namespace SystemName.Models
{//in progress Needs subgenre and timestamps
    public class Product
    {
        // Gets and sets values for Product
        public int ID { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter a Author.")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "Please enter a Description.")]
        public string? Description { get; set; }



        [Required(ErrorMessage = "Please select a genre.")]
        public int Genre { get; set; } //Gets genre foreign key as well as way to access object
        
        [ForeignKey("Genre"), ValidateNever]
        public virtual Genre GenreObj { get; set; } = null!;

        [Required(ErrorMessage = "Please select a subgenre.")]
        public int subGenre { get; set; }
        //ADD SubGenre --------------------------------------------------

        [Required(ErrorMessage = "Please enter a Year.")]
        [Range(typeof(DateTime), "1/1/1889", "1/1/2999")]
        public DateTime? Published { get; set; }
        // ADD last updated and by who ----------------------------------

        public string Slug =>
            Name?.Replace(' ', '-').ToLower() + '-' + Published?.ToString(); //adjusting names to be suitable
        


    }
}
