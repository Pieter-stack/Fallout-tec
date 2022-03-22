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

        public void OnGet()
        {
            InventoryItems = Database.GetInventory();
        }

        public IActionResult OnPostUpdate(string itemname, int itemcount, int itemlocation)
        { 
            Console.WriteLine($"{itemname}{itemcount}{itemlocation}");  
           Database.UpdateInvItemsQuantity(itemname, itemcount, itemlocation);

            //redirect
            return RedirectToPage("./inventorypage");
        }
        public void OnPostSearch(string search)
        {
            Console.WriteLine($"{search}");
            InventoryItems = Database.GetInputInvSearch(search);



        }
        public void OnPostLocation(int location)
        {
            Console.WriteLine($"{location}");
            InventoryItems = Database.GetInventorySearch(location);
          


        }








    }
}
