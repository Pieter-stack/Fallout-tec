using Fallout_tec.Models;
using Fallout_tec.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fallout_tec.Pages
{
    public class craftingpageModel : PageModel
    {
        public List<Recipe> allRecipes = new List<Recipe>();

        public string Message { get; set; } = string.Empty;
        public bool MessageSuccess { get; set; } = false;

        public void OnGet(string message = "", bool success = true)
        {
            allRecipes = new Recipebook().Recipes;

            Message = message;
            MessageSuccess = success;
        }

       public IActionResult OnPostCraft(string name, int count, int location, List<string> ingredientsName , List<string> ingredientsCount, string verify)
        {
            //TODO:call db function


            foreach (var i in ingredientsName)
            {
                Console.WriteLine(i);
            }
            foreach (var i in ingredientsCount)
            {
                Console.WriteLine(i);
            }




            var success = new Recipebook().CraftRecipe(name, count + 1, location, ingredientsName, ingredientsCount, verify);

            if (success)
            {
                return Redirect($"./Craftingpage?success=true&message=The item: {name} has been crafted");
            }
            else
            {
                return Redirect($"./Craftingpage?success=false&message=The item: {name} has not been crafted");
            }






        }
        public void OnPostLocation(int location, string station)
        {
            Console.WriteLine($"{location}");
            Console.WriteLine($"{station}");
            allRecipes = Database.GetCraftingPageSearch(location, station);



        }


    }
}
