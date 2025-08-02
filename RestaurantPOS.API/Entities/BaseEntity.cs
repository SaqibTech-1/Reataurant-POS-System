namespace RestaurantPOS.API.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public Guid GlobalId { get; set; } = Guid.NewGuid();
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
