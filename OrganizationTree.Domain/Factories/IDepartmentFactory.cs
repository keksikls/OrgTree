using OrganizationTree.Domain.Entity;
using OrganizationTree.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Domain.Factories
{
    public interface IDepartmentFactory
    {
        Task<Department> CreateAsync(string name, Guid? parentId, DepartmentType type);
    }
}
