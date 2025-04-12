using AutoMapper;
using MediatR;
using OrganizationTree.Application.Common;
using OrganizationTree.Application.DTO;
using OrganizationTree.Domain.Entity;
using OrganizationTree.Domain.Enums;
using OrganizationTree.Domain.Factories;
using OrganizationTree.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Application.Departments.Commands.Handler
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, ResultGen<Guid>>
    {
        private readonly IDepartmentFactory _factory;
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(IDepartmentFactory factory, IDepartmentRepository repository, IMapper mapper)
        {
            _factory = factory;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultGen<Guid>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return ResultGen<Guid>.Failure($"Name подразделения обезательно");
            }
            // мапим данные
            var department = _mapper.Map<Department>(request);

            // Дополнительные проверки/логика
            if (department.ParentId.HasValue &&
                !await _repository.ExistsAsync(department.ParentId.Value))
            {
                return ResultGen<Guid>.Failure("Родительское подразделение не найдено");
            }

            await _repository.AddAsync(department);
            await _repository.SaveChangesAsync(cancellationToken);

            return department.Id;
        }
    }
}
