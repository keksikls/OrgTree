using AutoMapper;
using FluentValidation;
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
        private readonly IValidator<CreateDepartmentCommand> _validator;

        public CreateDepartmentCommandHandler(IDepartmentFactory factory, IDepartmentRepository repository, IMapper mapper
            ,IValidator<CreateDepartmentCommand> validator)
        {
            _factory = factory;
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ResultGen<Guid>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            // Валидация команды
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                return ResultGen<Guid>.Failure(string.Join(", ", errorMessages));
            }

            // Проверка существования родителя
            if (request.ParentId.HasValue &&
                !await _repository.ExistsAsync(request.ParentId.Value, cancellationToken))
            {
                return ResultGen<Guid>.Failure("Родительское подразделение не найдено");
            }
            // мапим данные
            var department = _mapper.Map<Department>(request);

            await _repository.AddAsync(department, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return ResultGen<Guid>.Success(department.Id);
        }
    }
}
