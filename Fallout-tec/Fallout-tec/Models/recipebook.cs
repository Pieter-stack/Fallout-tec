using Fallout_tec.Services;

namespace Fallout_tec.Models
{
    public class Recipebook
    {

        public List<Recipe> Recipes = new List<Recipe>();
        public Recipebook()
        {

            //Todo: Call database
            Recipes = Database.GetAllRecipes();
        }

       public void CraftRecipe(string name, int count, string location, List<string> ingredientsName, List<string> ingredientsCount)
        {
            //TODO: call the database function
           Database.CraftRecipe(name, count, location, ingredientsName, ingredientsCount);


        }



    }
}
