﻿namespace Fallout_tec.Models
{
    public class linechart
    {
        public string ItemName { get; set; } = String.Empty;

        public string ItemType { get; set; } = String.Empty;

        public string ItemPicture { get; set; } = String.Empty;

        public string Location { get; set; } = String.Empty;

        private int ItemQuantity;

        public int itemquantity
        {
            get { return ItemQuantity; }
            set
            {
                if (value > 0)
                    ItemQuantity = value;
                else
                    ItemQuantity = 0;
            }
        }

        public linechart(int newCount)
        {
            itemquantity = newCount;

        }




    }
}
