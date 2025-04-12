using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Application.Departments.Commands.FluentValidatator
{
    public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название обязательно")
                .MaximumLength(20).WithMessage("Максимум 20 символов");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Недопустимый тип подразделения");
        }
    }
}
