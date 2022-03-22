using Fallout_tec.Models;
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

      //  public IActionResult OnPostCraft(string name, int count, List<string> ingredients)
     //   {
            //TODO:call db function
          //  new Recipebook().CraftRecipe(name, count + 1, ingredients);

         //   return Redirect($"./Crafting?message={name} has been crafted");
    //    }


    }
}
