using Fallout_tec.Models;
using Fallout_tec.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fallout_tec.Pages
{
    public class craftingpageModel : PageModel
    {
        public List<Recipe> allRecipes = new List<Recipe>();
        public void OnGet()
        {
            allRecipes = new Recipebook().Recipes;
        }

       public IActionResult OnPostCraft(string name, int count, string location, List<string> ingredientsName , List<string> ingredientsCount)
        {
            //TODO:call db function
                

            

            Console.WriteLine("count" + count);
            Console.WriteLine(name);
            Console.WriteLine("location" + location);

            foreach (var i in ingredientsName)
            {
                Console.WriteLine(i);
            }
            foreach (var i in ingredientsCount)
            {
                Console.WriteLine(i);
            }



            new Recipebook().CraftRecipe(name, count + 1, location, ingredientsName, ingredientsCount);
            return RedirectToPage("./craftingpage");

            //skryf function
            //for(var i = 0l i < name.length; i++{
            // name[i] same wees as count[i]
            //}




        }
        public void OnPostLocation(int location, string station)
        {
            Console.WriteLine($"{location}");
            Console.WriteLine($"{station}");
            allRecipes = Database.GetCraftingPageSearch(location, station);



        }


    }
}
