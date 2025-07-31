namespace FormApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Image { get; set; }
        public bool IsActive { get; set; }
        public int categoryId { get; set; }



    }
}
