using Microsoft.AspNetCore.Mvc;
using OrganizationTree.Application.Interfaces;
using OrganizationTree.Domain.Interfaces;
using System.Text;

namespace OrganizationTree.Web.Controllers
{
    public class DepartmentsXmlController : Controller
    {
        private readonly IDepartmentXmlService _xmlService;
        private readonly IDepartmentRepository _repository;

        public DepartmentsXmlController(IDepartmentXmlService xmlService, IDepartmentRepository repository)
        {
            _xmlService = xmlService;
            _repository = repository;
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportXml()
        {
            var departments = await _repository.GetAllAsync();
            var xml = _xmlService.ExportToXml(departments);

            return File(Encoding.UTF8.GetBytes(xml),
                   "application/xml",
                   $"departments_{DateTime.Now:yyyyMMdd}.xml");
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportXml(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            using var reader = new StreamReader(file.OpenReadStream());
            var xml = await reader.ReadToEndAsync();

            try
            {
                var departments = _xmlService.ImportFromXml(xml);
                await _repository.AddRangeAsync(departments);
                return Ok($"Imported {departments.Count} departments");
            }
            catch (Exception ex)
            {
                return BadRequest($"Import failed: {ex.Message}");
            }
        }
    }
}
