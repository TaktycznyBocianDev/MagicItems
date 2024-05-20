namespace MagicItems.Shared.Models
{
    public partial class Items
    {
        public int Id { get; set; }
        public string ItemName { get; set; } = "New Item";
        public string ShortDescription { get; set; } = "Short Desc of new item";
        public string LongDescription { get; set; } = "Long Decs of new item";
        public int Price { get; set; }
        public string CategoryName { get; set; } = "Wonderous Item";
        public string RarityName { get; set; } = "Common";

        //For MudSelect
        public override bool Equals(object? o)
        {
            var other = o as Items;
            return other?.ItemName == ItemName;
        }

        public override int GetHashCode() => ItemName?.GetHashCode() ?? 0;

        public override string ToString() => ItemName;

    }
}