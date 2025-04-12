using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OrganizationTree.Web.ViewModels
{
    public class MoveDepartmentModel
    {
        [Required(ErrorMessage = "Необходимо указать нового родителя")]
        [Display(Name = "Новое родительское подразделение")]
        public Guid? NewParentId { get; set; }

        public SelectList? AvailableDepartments { get; set; }
    }
}
