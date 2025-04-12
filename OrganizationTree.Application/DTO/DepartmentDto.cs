using OrganizationTree.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Application.DTO
{
    public class DepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int OrderNumber { get; set; }
        public DepartmentType Type { get; set; }
        public Status Status { get; set; }
        public Guid? ParentId { get; set; }
        public string? ParentName { get; set; }
    }
}
