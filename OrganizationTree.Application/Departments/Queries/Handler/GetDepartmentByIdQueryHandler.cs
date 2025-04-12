using AutoMapper;
using MediatR;
using OrganizationTree.Application.Common;
using OrganizationTree.Application.DTO;
using OrganizationTree.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Application.Departments.Queries.Handler
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, ResultGen<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public GetDepartmentByIdQueryHandler(
            IDepartmentRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResultGen<DepartmentDto>> Handle(
            GetDepartmentByIdQuery query,
            CancellationToken ct)
        {
            var department = await _repository.GetByIdWithParentAsync(query.Id, ct);

            return department == null
                ? ResultGen<DepartmentDto>.Failure("Подразделение не найдено")
                : _mapper.Map<DepartmentDto>(department);
        }
    }
}
