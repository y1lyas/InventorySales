using InventorySales.Domain.Entities.Common;
using InventorySales.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace InventorySales.Domain.Entities.Auth
{

    public class User : BaseEntity<Guid>
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
