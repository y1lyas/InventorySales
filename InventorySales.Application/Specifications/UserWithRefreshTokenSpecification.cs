using InventorySales.Application.Specifications.Base;
using InventorySales.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySales.Application.Specifications
{
    public class UserWithRefreshTokenSpecification : BaseSpecification<User>
    {
        public UserWithRefreshTokenSpecification(Guid userId)
        {
            Criteria = u => u.Id == userId;
            AddInclude(u => u.RefreshTokens);
        }
    }

}
