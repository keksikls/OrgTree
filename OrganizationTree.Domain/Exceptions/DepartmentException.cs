using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Domain.Exceptions
{
    public class DepartmentException : Exception
    {
        public DepartmentException(string message) : base(message)
        {
        }

        public class DepartmentHasActiveEmployeesException : DepartmentException
        {
            public DepartmentHasActiveEmployeesException() : base("Нельзя деактивировать подразделения с аткивными сотрудинками")
            {
            }
        }

        public class InvalidDepartmentNameException : DepartmentException
        {
            public InvalidDepartmentNameException(string name): base($"неправильное название подразделения: {name}") { }
        }

        public class ParentDepartmentNotFoundException : DepartmentException
        {
            public ParentDepartmentNotFoundException(Guid parentId): base($"главное подразделение с ID {parentId} не найдено") { }
        }

        public class DepartmentLimitExceededException : DepartmentException
        {
            public DepartmentLimitExceededException(Guid? parentId, string message)
                : base($"Лимит подразделений для {parentId}: {message}") { }
        }

        public class OrderNumberInconsistencyException : DepartmentException
        {
            public OrderNumberInconsistencyException(string message)
                : base($"Ошибка нумерации: {message}") { }
        }
    }
}
