namespace CatalogAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Categories { get; set; } = new();
        public string ImageFile { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }
}
