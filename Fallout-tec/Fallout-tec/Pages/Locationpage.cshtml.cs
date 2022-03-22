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
        public List<Inventory> InventoryItems = new List<Inventory>();





        public void OnGet()
        {
            //convert to json
            JsonConvert.SerializeObject(InventoryItems);



            InventoryItems = Database.GetInventory();
        }





    }
}
