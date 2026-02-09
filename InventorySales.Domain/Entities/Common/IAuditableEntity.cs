namespace InventorySales.Domain.Entities.Common
{
    public interface IAuditableEntity
    {
        string CreatedById { get; set; }
        string? ModifiedById { get; set; }
        DateTime? ModifiedAt { get; set; }

    }
}
