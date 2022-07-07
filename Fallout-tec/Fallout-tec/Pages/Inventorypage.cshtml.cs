using Fallout_tec.Models;
using Fallout_tec.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fallout_tec.Pages
{
    public class InventorypageModel : PageModel
    {

        //inventory variable we call from our frontend
        public List<Inventory> InventoryItems = new List<Inventory>();

        public InventorypageModel()
        {


        }
        //https://stackoverflow.com/questions/14956027/how-to-pass-values-across-the-pages-in-asp-net-without-using-session
        public void OnGet(int location)
        {
            InventoryItems = Database.GetInventory();

        }
        //1. pass the query param in die nav bar link
        //2. Layout n on click function add vir die link, wat die localstorage clear
        public void OnGetLocation(int location)
        {
            Console.WriteLine($"{location}");
            InventoryItems = Database.GetCraftingSearch(location);

        }

        public void OnPostUpdate(string itemname, int itemcount, int itemlocation)
        { 
            Console.WriteLine($"{itemname}{itemcount}{itemlocation}");  
           Database.UpdateInvItemsQuantity(itemname, itemcount, itemlocation);
            InventoryItems = Database.GetCraftingSearch(itemlocation);


        }
        public void OnPostSearch(string search, int location)
        {
            Console.WriteLine($"{search}");
            Console.WriteLine($"{location}");
            InventoryItems = Database.GetInputInvSearch(search, location);
            //search must be refined to only search for letters and also name needs tro be static after search


        }
        public void OnPostLocation(int location)
        {
            Console.WriteLine($"{location}");
            InventoryItems = Database.GetCraftingSearch(location);
          


        }








    }
}
