using OrganizationTree.Application.Interfaces;
using OrganizationTree.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OrganizationTree.Infrastructure.Services
{
    public class DepartmentXmlService : IDepartmentXmlService
    {
        public string ExportToXml(List<Department> departments)
        {
            var serializer = new XmlSerializer(typeof(List<Department>));
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", ""); // Убираем namespace по умолчанию

            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, departments, namespaces);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        public List<Department> ImportFromXml(string xmlContent)
        {
            var serializer = new XmlSerializer(typeof(List<Department>));

            using (var reader = new StringReader(xmlContent))
            {
                return (List<Department>)serializer.Deserialize(reader)!;
            }
        }
    }
}
