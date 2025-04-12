using MediatR;
using OrganizationTree.Application.Common;
using OrganizationTree.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Application.Departments.Queries
{
    public record GetDepartmentByIdQuery(Guid Id) : IRequest<ResultGen<DepartmentDto>>
    {
    }
}
