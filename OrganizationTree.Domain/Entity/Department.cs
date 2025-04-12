using OrganizationTree.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Domain.Entity
{
    public class Department
    {
        public Guid Id { get;  set; }
        [Required, MaxLength(20, ErrorMessage = "Название не может быть длиннее 20 символов")]
        public string Name { get;set; }
        [Range(1, 50, ErrorMessage = "Порядковый номер должен быть от 1 до 50")]
        public int OrderNumber { get;set; }
        public Guid? ParentId { get; set; }
        public DepartmentType Type { get;set; }
        public Status Status { get;  set; }

        // Навигационные свойства
        public Department? Parent { get; set; }
        public ICollection<Department> Children { get; set; } = new List<Department>();

        // Приватный конструктор для EF Core
        public Department(string name, Guid? parentId, DepartmentType type, int orderNumber)
        {
            Id = Guid.NewGuid();
            Name = name;
            ParentId = parentId;
            Type = type;
            OrderNumber = orderNumber;
            Status = Status.Active;
        }
        //пустой для ef
        protected Department() { }
    }
}
