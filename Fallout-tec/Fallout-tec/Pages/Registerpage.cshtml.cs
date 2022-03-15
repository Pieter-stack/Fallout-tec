using Fallout_tec.Models;
using Fallout_tec.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fallout_tec.Pages
{
    public class RegisterpageModel : PageModel
    {

        public List<Users> RegUsers = new List<Users>();
        public void OnGet()
        {
            RegUsers = Database.GetUsers();
        }
        public IActionResult OnPostRegister(string username, string email, string password, string profilepic)
        {
            Database.RegisterNewUser(username, email, password, profilepic);

            //redirect
            return RedirectToPage("./inventorypage");
        }
    }




}
