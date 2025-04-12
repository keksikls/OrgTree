using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Domain.Services
{
    public interface IOrderNumberGenerator
    {
        Task<int> GetNextAsync(Guid? parentId, CancellationToken ct);
        Task<int> GetNextAsync(Guid? newParentId);
    }
}
