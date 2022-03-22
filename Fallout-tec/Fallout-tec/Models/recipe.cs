using Fallout_tec.Interfaces;

namespace Fallout_tec.Models
{
    public class Recipe : Craftable
    {
        private int count;

        //recipe constructor

        public Recipe(int newCount)
        {
            count = newCount;
        }

        public string CraftName { get; set; } = string.Empty;
        public string CraftImage { get; set; } = string.Empty;
        public string CraftType { get; set; } = string.Empty;
        public string CraftStation { get; set; } = string.Empty;

        public int Count { get { return count; } }
        //TODO : add Ingredients property


        public List<string> IngredientsName { get; set; }
        public List<string> IngredientsCount { get; set; }
        public List<string> IngredientsImage { get; set; }


        public bool IsCraftable()
        {
            //TODO: check if we have all the required resources
            //setup empty dictionary  which will contain item and number
            var map = new Dictionary<string, int>();

            foreach (var ingredient in IngredientsName)
            {//loop though all ingerdients add to dictionary
                if (ingredient != "")
                {
                    int count;
                    if (map.TryGetValue(ingredient, out count))//have we added this ingredient before
                    {
                        map[ingredient] += 1; //increment count
                    }
                    else //not added ingredient to dictionary
                    {
                        map.Add(ingredient, 1);  //add the block to it
                    }
                }
            }

            //default response
            bool result = true;
            //go create our inventory that we check
            var inventory = new Materials();
            //loop through our ingredient dictionary
            foreach (var pair in map)
            {
                Console.WriteLine(pair);
                if (pair.Value > inventory.GetCount(pair.Key))//if amount we need ids less than what we have craftable faalse
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
