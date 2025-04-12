
using OrganizationTree.Domain.Entity;
using OrganizationTree.Domain.Enums;
using OrganizationTree.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Domain.Factories
{
    public class DepartmentFactory : IDepartmentFactory 
    {
        private readonly IOrderNumberGenerator _orderNumberGenerator;
        public DepartmentFactory(IOrderNumberGenerator orderNumberGenerator)
        {
            _orderNumberGenerator = orderNumberGenerator;
        }
        public async Task<Department> CreateAsync(string name, Guid? parentId, DepartmentType type)
        {
            return new Department
            {
                Id = Guid.NewGuid(),
                Name = name,
                ParentId = parentId,
                Type = type,
                OrderNumber = await _orderNumberGenerator.GetNextAsync(parentId),
                Status = Status.Active
            };
        }
    }
}
