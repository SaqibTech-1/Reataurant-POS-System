namespace RestaurantPOS.API.Entities
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; } 
    }
}
