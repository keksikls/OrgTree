using Microsoft.AspNetCore.Mvc;
using OrganizationTree.Domain.Entity;
using OrganizationTree.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Domain.Services
{
    public interface IDepartmentService
    {
        Task MoveAsync(Department department, Guid? newParentId, CancellationToken ct);
        Task UpdateAsync(Department department, string name, DepartmentType type);
        Task DeactivateAsync(Department department);
    }
}
