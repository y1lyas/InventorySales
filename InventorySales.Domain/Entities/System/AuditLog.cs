namespace InventorySales.Domain.Entities.System
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; } = null!;
        public string EntityId { get; set; } = null!;
        public string Action { get; set; } = null!;
        public string? ChangedProperties { get; set; }
        public string PerformedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public string CorrelationId { get; set; } = null!;
    }

}
