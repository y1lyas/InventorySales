namespace InventorySales.Domain.Entities.Common.Interfaces
{
    public interface IAuditable
    {
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedById { get; set; }

    }
}
