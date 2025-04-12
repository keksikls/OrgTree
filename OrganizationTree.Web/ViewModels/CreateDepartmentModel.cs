using Microsoft.AspNetCore.Mvc.Rendering;
using OrganizationTree.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrganizationTree.Web.ViewModels
{
    public class CreateDepartmentModel
    {
        [Required(ErrorMessage = "Название обязательно")]
        [StringLength(20, ErrorMessage = "Не более 20 символов")]
        public string Name { get; set; }

        [Display(Name = "Родительское подразделение")]
        public Guid? ParentId { get; set; }

        [Required(ErrorMessage = "Тип обязателен")]
        public DepartmentType Type { get; set; } = DepartmentType.Department;

        // Для выпадающего списка
        public SelectList? AvailableDepartments { get; set; }
    }
}
