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
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public ICollection<Role> Roles { get; private set; } = new List<Role>();
        public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();

        public User() { }

        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
        }
        public void Register(string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("Email or password cannot be empty.");

                Email = email;
            PasswordHash = passwordHash;
        }
        public void SetPasswordHash(string hash) => PasswordHash = hash;
        public void AddRole(Role role)
        {
            if (!Roles.Any(r => r.Name == role.Name))
                Roles.Add(role);
        }
        public RefreshToken AddRefreshToken(string token, DateTime expires)
        {
            var r = new RefreshToken(token, expires);
            RefreshTokens.Add(r);
            return r;
        }
        public void RevokeRefreshToken(string token)
        {
            var rt = RefreshTokens.FirstOrDefault(x => x.Token == token);
            if (rt != null) rt.Revoke();
        }
    }
}
