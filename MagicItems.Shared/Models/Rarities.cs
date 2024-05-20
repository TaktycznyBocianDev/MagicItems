namespace MagicItems.Shared.Models
{
    public partial class Rarities
    {
        public int Id { get; set; }
        public string RarityName { get; set; } = "New Rarity";

        //For MudSelect
        public override bool Equals(object? o)
        {
            var other = o as Rarities;
            return other?.RarityName == RarityName;
        }

        public override int GetHashCode() => RarityName?.GetHashCode() ?? 0;

        public override string ToString() => RarityName;
    }
}