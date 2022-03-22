using Fallout_tec.Services;

namespace Fallout_tec.Models
{
    public class Materials
    {
        public List<Inventory> InventoryItems = new List<Inventory>();
        public Materials()
        {
            InventoryItems = Database.GetInventory();

        }

        public int GetCount(string name)
        {
            foreach (var item in InventoryItems)//seatching for specific block
            {
                if (item.ItemName == name)
                {
                    return item.itemquantity;
                }

            }
            return -1; //because there are no blocks with that name
        }


    }
}
