using Fallout_tec.Models;
using Fallout_tec.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fallout_tec.Pages
{
    public class RegisterpageModel : PageModel
    {



        public List<Users> RegUsers = new List<Users>();
        public string Message { get; set; } = string.Empty;
        public bool MessageSuccess { get; set; } = false;
        public void OnGet(string message = "", bool success = true)
        {
            RegUsers = Database.GetUsers();

            Message = message;
            MessageSuccess = success;

        }


        public IActionResult OnPostRegister(string username, string email, string password)
        {

            var success = Database.RegisterNewUser(username, email, password);

            if (success)
            {
                return Redirect($"./InventoryPage?success=true&message={username} has been registered");
            }
            else
            {
                return Redirect($"./Registerpage?success=false&message=The email: {email} is already registered");
            }

        }
    }




}
