using Microsoft.EntityFrameworkCore;
using OrganizationTree.Domain.Entity;
using OrganizationTree.Domain.Enums;
using OrganizationTree.Domain.Exceptions;
using OrganizationTree.Domain.Interfaces;
using OrganizationTree.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
        }

        public async Task AddRangeAsync(IEnumerable<Department> departments, CancellationToken cancellationToken = default)
        {
            await _context.Departments.AddRangeAsync(departments, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id)
        {
            var department = await GetByIdAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Departments.AnyAsync(x => x.Id == id);
        }

        public async Task<List<Department>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Departments
                  .AsNoTracking() // Для read-only операций
                  .Include(d => d.Parent) // Подгружаем родительские подразделения
                  .Include(d => d.Children) // Подгружаем дочерние подразделения
                  .OrderBy(d => d.OrderNumber) // Сортируем по порядковому номеру
                  .ToListAsync(cancellationToken);
        }

        public async Task<Department?> GetByIdAsync(Guid id)
        {
            return await _context.Departments.FirstOrDefaultAsync(u=>u.Id == id);
        }

        public Task<Department?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<Department?> GetByIdWithParentAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Departments
                  .AsNoTracking() // не остоеживаю
                  .Include(d => d.Parent) // связанные подразделения
                    .FirstOrDefaultAsync(d => d.Id == id, cancellationToken); // naxodim
        }

        public async Task<List<Department>> GetChildrenAsync(Guid? parentId)
        {
            return await _context.Departments
                .Where(d => d.ParentId == parentId)
                .OrderBy(d => d.OrderNumber)
                .ToListAsync();
        }

        public async Task<List<Department>> GetRootDepartmentsAsync()
        {
            return await _context.Departments
                .Where(d => d.ParentId == null)
                .ToListAsync();
        }

        public async Task<bool> HasActiveEmployeesAsync(Guid departmentId)
        {
            return await _context.Departments
                  .AnyAsync(e => e.ParentId == departmentId && e.Status == Status.Active);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                // Сохраняем изменения и возвращаем количество затронутых строк
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("Ошибка параллельного доступа", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new PersistenceException("Ошибка сохранения данных", ex);
            }
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
        }
    }
}
