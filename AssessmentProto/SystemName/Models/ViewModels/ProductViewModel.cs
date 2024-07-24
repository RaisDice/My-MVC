
namespace SystemName.Models.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; }

        public List<Stocktake> Stocktakes { get; set; }
        public List<Book_genre> Book_Genres { get; set; }
        public List<Movie_genre> Movie_Genres { get; set; }
        public List<Game_genre> Game_Genres { get; set; }



        public int GetQuantity(Product product) //using relevant ID to find quantity in stocktakes
        {
            var stocktake = Stocktakes.FirstOrDefault(x => x.ProductId == product.ID); 
            if (stocktake != null)
            {
                return stocktake.Quantity;
            }
            else
            {
                return 0;
            }
        }

        public double GetPriceForProduct(Product product) //using relevant ID to find price in stocktakes
        {
            var stocktake = Stocktakes.FirstOrDefault(x => x.ProductId == product.ID);
            if (stocktake != null)
            {
                double help = stocktake.Price;
                return help;
            }
            return 0; // Return the price as float, or 0.0 if not found
        }

        public string GetSubGenreName(Product product)
        {
            //https://stackoverflow.com/questions/70452428/entity-framework-where-clause-filtering-from-specific-column search based on condition
            //https://stackoverflow.com/questions/57589291/iqueryable-does-not-contain-a-definition-for-singleordefault Where() to SingleOrDefault()
            string subgenreName = ""; // Default value if not found
            switch (product.Genre)
            {
                case 1:
                    var bookGenre = Book_Genres.FirstOrDefault(x => x.subGenreID == product.subGenre);
                    if (bookGenre != null)
                    {
                        subgenreName = bookGenre.Name;
                    }
                    break;
                case 2:
                    var movieGenre = Movie_Genres.FirstOrDefault(x => x.subGenreID == product.subGenre);
                    if (movieGenre != null)
                    {
                        subgenreName = movieGenre.Name;
                    }
                    break;

                case 3:
                    var gameGenre = Game_Genres.FirstOrDefault(x => x.subGenreID == product.subGenre);
                    if (gameGenre != null)
                    {
                        subgenreName = gameGenre.Name;
                    }
                    break;
                default:
                    subgenreName = "Test";
                    break;
            }

            // Add more cases for other genres as needed.

            return subgenreName;
        }
    }
   
}
