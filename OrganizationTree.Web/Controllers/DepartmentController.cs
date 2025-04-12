using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrganizationTree.Application.Departments.Commands;
using OrganizationTree.Application.Departments.Queries;
using OrganizationTree.Web.ViewModels;
namespace OrganizationTree.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet] // GET: Departments/Create
        public IActionResult Create()
        {
            return View(new CreateDepartmentModel());
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var command = new CreateDepartmentCommand(model.Name, model.ParentId, model.Type);
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Error);
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Departments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteDepartmentCommand(id));

            if (!result.IsSuccess)
                TempData["Error"] = result.Error;

            return RedirectToAction(nameof(Index));
        }

        // POST: Departments/Move/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Move(Guid id, MoveDepartmentModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Edit), new { id });

            var result = await _mediator.Send(new MoveDepartmentCommand(id, model.NewParentId));

            if (!result.IsSuccess)
                TempData["Error"] = result.Error;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]// GET: Departments
        public async Task<IActionResult> Index()
        {
            var departments = await _mediator.Send(new GetAllDepartmentsQuery());
            return View(departments);
        }

        [HttpGet] // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _mediator.Send(new GetDepartmentByIdQuery(id));

            if (!result.IsSuccess)
                return RedirectToAction(nameof(Index));

            return View(result.Value);
        }

    }
}
