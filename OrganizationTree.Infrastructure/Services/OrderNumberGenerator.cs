using OrganizationTree.Domain.Interfaces;
using OrganizationTree.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrganizationTree.Domain.Exceptions.DepartmentException;

namespace OrganizationTree.Infrastructure.Services
{
    public class OrderNumberGenerator : IOrderNumberGenerator
    {
        private readonly IDepartmentRepository _repository;
        private readonly SemaphoreSlim _semaphore = new (1, 1);

        public OrderNumberGenerator(IDepartmentRepository repository)
        {
            _repository = repository;
        }
        public async Task<int> GetNextAsync(Guid? parentId)
        {
            await _semaphore.WaitAsync();

            if (parentId.HasValue)
            {
                bool parentExists = await _repository.ExistsAsync(parentId.Value);
                if (!parentExists)
                {
                    throw new ParentDepartmentNotFoundException(parentId.Value);
                }
            }

            await _semaphore.WaitAsync();
            try
            {
                var children = await _repository.GetChildrenAsync(parentId);

                // Проверка максимального количества подразделений
                if (children.Count >= 50)
                {
                    throw new DepartmentLimitExceededException(parentId,"лимит подразделений (50) для главного элемента");
                }

                var maxNumber = children.Max(d => (int?)d.OrderNumber) ?? 0;
                int nextNumber = maxNumber + 1;

                // Дополнительная проверка согласованности номеров
                if (children.Any() && nextNumber != children.Count + 1)
                {
                    throw new OrderNumberInconsistencyException("ошибка присваивания OrderNumber");
                }

                return Math.Clamp(nextNumber, 1, 50);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public Task<int> GetNextAsync(Guid? parentId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
