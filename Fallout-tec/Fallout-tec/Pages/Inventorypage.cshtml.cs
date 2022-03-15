using Fallout_tec.Models;
using Fallout_tec.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fallout_tec.Pages
{
    public class InventorypageModel : PageModel
    {

        //blocks variable we call from our frontend
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

        public IActionResult OnPostSearchpack(int backpack)
        {
            Console.WriteLine($"{backpack}");  
            Database.SearchUsersbackpack(backpack);

            //redirect
            return RedirectToPage("./inventorypage");
        }
        public IActionResult OnPostSearchbase( int homebase)
        {
            Console.WriteLine($"{homebase}");
            Database.SearchUsersbase(homebase);

            //redirect
            return RedirectToPage("./inventorypage");
        }
        public IActionResult OnPostSearchtent( int survivaltent)
        {
            Console.WriteLine($"{survivaltent}");
            Database.SearchUserstent(survivaltent);

            //redirect
            return RedirectToPage("./inventorypage");
        }

    }
}
