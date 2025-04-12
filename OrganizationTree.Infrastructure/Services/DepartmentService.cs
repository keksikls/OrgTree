using OrganizationTree.Domain.Entity;
using OrganizationTree.Domain.Enums;
using OrganizationTree.Domain.Interfaces;
using OrganizationTree.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static OrganizationTree.Domain.Exceptions.DepartmentException;

namespace OrganizationTree.Infrastructure.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IOrderNumberGenerator _orderNumberGenerator;
        private readonly IDepartmentRepository _repository;


        public DepartmentService(IOrderNumberGenerator orderNumberGenerator, IDepartmentRepository repository)
        {
            _orderNumberGenerator = orderNumberGenerator;
            _repository = repository;
        }

        public async Task DeactivateAsync(Department department)
        {
            var hasActive = await _repository.HasActiveEmployeesAsync(department.Id);

            if (hasActive)
            {
                throw new DepartmentHasActiveEmployeesException();
            }

            department.Status = Status.inActive;
        }

        public async Task MoveAsync(Department department, Guid? newParentId)
        {
            department.ParentId = newParentId;
            department.OrderNumber = await _orderNumberGenerator.GetNextAsync(newParentId);
        }

        public async Task MoveAsync(Department department, Guid? newParentId, CancellationToken ct)
        {
            department.ParentId = newParentId;
            department.OrderNumber = await _orderNumberGenerator.GetNextAsync(newParentId, ct);
        }
        public Task UpdateAsync(Department department, string name, DepartmentType type)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidDepartmentNameException(name);

            department.Name = name;
            department.Type = type;
            return Task.CompletedTask;
        }
    }
}
