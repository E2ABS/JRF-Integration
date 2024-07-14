// Models/ItemStock.cs
namespace YourNamespace.Models
{
    public class ItemStock
    {
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string BarCode { get; set; }
        public string ItemGroup { get; set; }
        public string ItemUOM { get; set; }
        public decimal? Price { get; set; } // Nullable in case price is not defined
        public decimal InStock { get; set; }
        public decimal Committed { get; set; }
        public decimal AvailableQuantity { get; set; }
    }
}
