using MediatR;
using OrganizationTree.Application.Common;
using OrganizationTree.Domain.Interfaces;
using OrganizationTree.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Application.Departments.Commands.Handler
{
    public class MoveDepartmentCommandHandler : IRequestHandler<MoveDepartmentCommand, ResultGen<Unit>>
    {
        private readonly IDepartmentService _departmentService;
        private readonly IDepartmentRepository _repository;

        public MoveDepartmentCommandHandler(
            IDepartmentService departmentService,
            IDepartmentRepository repository)
        {
            _departmentService = departmentService;
            _repository = repository;
        }

        public async Task<ResultGen<Unit>> Handle(MoveDepartmentCommand request,CancellationToken ct)
        {
            var department = await _repository.GetByIdAsync(request.DepartmentId, ct);
            if (department == null)
                return ResultGen<Unit>.Failure("Подразделение не найдено");

            // Использование сервиса для бизнес-логики
            await _departmentService.MoveAsync(department, request.NewParentId, ct);

            await _repository.SaveChangesAsync(ct);
            return ResultGen<Unit>.Success(Unit.Value);
        }
    }
}
