using InventorySales.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Domain.Entities.Auth
{
    public class RefreshToken : BaseEntity<int>
    {
        public string Token { get; private set; }
        public DateTime ExpiresAt { get; private set; }
        public DateTime? RevokedAt { get; private set; }
        private RefreshToken() { }

        public RefreshToken(string token, DateTime expiresAt)
        {
            Token = token;
            ExpiresAt = expiresAt;
        }
        public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiresAt;
        public void Revoke() => RevokedAt = DateTime.UtcNow;
    }
}
