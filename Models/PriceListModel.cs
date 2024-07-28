using System.Collections.Generic;

namespace YourNamespace.Models
{
    public class PriceList
    {
        public int PriceListCode { get; set; }
        public string PriceListName { get; set; }
        public List<PriceListItem> Items { get; set; } = new List<PriceListItem>();
    }

    public class PriceListItem
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public List<UoMPrice> UoMPrices { get; set; } = new List<UoMPrice>();
    }

    public class UoMPrice
    {
        public string UoM { get; set; }

        public string UoMName { get; set; }
        public decimal Price { get; set; }
    }
}
