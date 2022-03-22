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
            //http://csharp.net-informations.com/file/csharp-filestream-class.htm
            //https://www.completecsharptutorial.com/basic/c-filestream-tutorial-with-programming-example.php
            //    FileStream f = new FileStream("~images/", FileMode.OpenOrCreate);//creating file stream  
            //   f.Write(profilepic);
            //   f.Close();//closing stream  

            //  Console.WriteLine(profilepic);

            Database.RegisterNewUser(username, email, password, profilepic);

            //redirect
            return RedirectToPage("./inventorypage");
        }
    }




}
