using MediatR;
using OrganizationTree.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Application.Departments.Commands
{
    public record DeleteDepartmentCommand(Guid Id) : IRequest<ResultGen<Unit>>;
}
