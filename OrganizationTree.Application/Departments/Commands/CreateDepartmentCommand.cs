using MediatR;
using OrganizationTree.Application.Common;
using OrganizationTree.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Application.Departments.Commands
{
    public record CreateDepartmentCommand(string Name, Guid? ParentId,DepartmentType Type) : IRequest<ResultGen<Guid>>;


}
