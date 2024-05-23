namespace MagicItems.Shared.Models
{
    public partial class ItemsDTO
    {
        public string ItemName { get; set; } = "New Item";
        public string ShortDescription { get; set; } = "Short Desc of new item";
        public string LongDescription { get; set; } = "Long Decs of new item";
        public int Price { get; set; } = 100;
        public string CategoryName { get; set; } = "Wonderous Item";
        public string RarityName { get; set; } = "Common";


    }
}