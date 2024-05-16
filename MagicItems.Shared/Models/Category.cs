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


    }
}