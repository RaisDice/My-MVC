using SystemName.Models;

public class AccountViewModel
{
    public List<TO> Customer { get; set; }

    public List<User> Users { get; set; }



    //public int GetQuantity(Product product)
    //{
    //    var stocktake = Stocktakes.FirstOrDefault(x => x.ProductId == product.ID);
    //    return stocktake.Quantity;
    //}

    public string GetNameForAccount(TO Customer) // gets TO item then Searchs by email to find name from Users
    {
        var user = Users.FirstOrDefault(u => u.Email == Customer.Email);// gets name from users if same email
        if (user != null)
        {
            string help = user.Name; 
            return help;
        }
        return "N/A"; // Return N/A no relavant emails
    }


}