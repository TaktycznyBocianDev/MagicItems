namespace MagicItems.Shared.Models
{
    public partial class ItemsDTO
    {
        public int CategoryId { get; set; }
        public string ItemName { get; set; } = "New Item";
        public string ShortDescription { get; set; } = "Short Desc of new item";
        public string LongDescription { get; set; } = "Long Decs of new item";
        public int Price { get; set; }
        public int RarityId { get; set; }


    }
}