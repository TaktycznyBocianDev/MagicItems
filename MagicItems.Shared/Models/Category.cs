using System.Xml.Linq;

namespace MagicItems.Shared.Models
{
    public partial class Category
    {
        public Category() { }
        public Category(int id, string categoryName)
        {
            Id = id;
            CategoryName = categoryName;
        }

        public int Id { get; set; }
        public string CategoryName { get; set; } = "New Category";

        //For MudSelect
        public override bool Equals(object? o)
        {
            var other = o as Category;
            return other?.CategoryName == CategoryName;
        }

        public override int GetHashCode() => CategoryName?.GetHashCode() ?? 0;

        public override string ToString() => CategoryName;

    }
}