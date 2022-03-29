using Fallout_tec.Models;
using Fallout_tec.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Fallout_tec.Pages
{
    public class loactionpageModel : PageModel
    {
        //inventory variable we call from our frontend
        public List<Inventory> InventoryItemsJunk = new List<Inventory>();
        public List<Inventory> InventoryItemsExtras = new List<Inventory>();
        public List<Inventory> InventoryItemsCraftable = new List<Inventory>();




        public void OnGet(int location)
        {

            InventoryItemsJunk = Database.GetInventoryJunk(1);
            InventoryItemsExtras = Database.GetInventoryExtras(1);
            InventoryItemsCraftable = Database.GetInventoryCraftable(1);
        }


        public void OnPostLocation(int location)
        {
            InventoryItemsJunk = Database.GetInventoryJunk(location);
            InventoryItemsExtras = Database.GetInventoryExtras(location);
            InventoryItemsCraftable = Database.GetInventoryCraftable(location);



        }





    }
}
