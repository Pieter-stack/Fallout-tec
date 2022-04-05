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

       public bool CraftRecipe(string name, int count, int location, List<string> ingredientsName, List<string> ingredientsCount, string verify)
        {
            //TODO: call the database function
      return  Database.CraftRecipe(name, count, location, ingredientsName, ingredientsCount, verify);




        }



    }
}
