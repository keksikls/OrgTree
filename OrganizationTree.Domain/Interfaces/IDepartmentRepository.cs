using OrganizationTree.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Domain.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<List<Department>> GetChildrenAsync(Guid? parentId); // Метод для получения детей
        Task<List<Department>> GetRootDepartmentsAsync();        
        Task AddAsync(Department department, CancellationToken cancellationToken);
        Task UpdateAsync(Department department);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> HasActiveEmployeesAsync(Guid departmentId);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<Department?> GetByIdWithParentAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Department>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<Department> departments, CancellationToken cancellationToken = default);
    }
}
