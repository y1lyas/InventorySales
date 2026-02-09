using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.Exceptions;


namespace InventorySales.Domain.Entities.Auth
{

    public class User : BaseEntity
    {
        public string ExternalId { get; private set; }
        public string? Email { get; set; }
        private User() { }
        public static User Create(string externalId, string? email)
        {
            if (string.IsNullOrWhiteSpace(externalId))
                throw new DomainException("ExternalId boş olamaz");

            return new User
            {
                Id = Guid.NewGuid(),
                ExternalId = externalId,
                Email = email
            };

        }
    }
}
