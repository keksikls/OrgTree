using OrganizationTree.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationTree.Application.Interfaces
{
    public interface IDepartmentXmlService
    {
        string ExportToXml(List<Department> departments);
        List<Department> ImportFromXml(string xmlContent);
    }
}
