using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SystemName.Models;

namespace SystemName.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        { }
        // allows Connection to tables
        public DbSet<Product> Product { get; set; } = null!;
        public DbSet<Genre> Genre { get; set; } = null!;
        public DbSet<Book_genre> Book_genre { get; set; } = null!;
        //public DbSet<Book_genre_NEW> Book_genre_NEW { get; set; } = null!;
        public DbSet<Game_genre> Game_genre { get; set; } = null!;
        public DbSet<Movie_genre> Movie_genre { get; set; } = null!;





    }
}