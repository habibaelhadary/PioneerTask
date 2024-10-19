using Application_Layer.Dtos.Employee;
using Application_Layer.Services.EmpServices;
using Application_Layer.Services.Propert;
using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Controllers.EmployeeControllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IPropertyService _propertyService;
        public EmployeeController(IEmployeeService employeeService, IPropertyService propertyService)
        {
            _employeeService = employeeService;
            _propertyService = propertyService;
        }
        public async Task<IActionResult> Index()
        {
            var EmpAll = await _employeeService.GetAllEmployee();
            return View(EmpAll);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var prop = await _propertyService.GetAllProperty();
            var model = new GetEmployeeDto
            {
                Properties = prop
            };
            return View(model);


        }
        [HttpPost]
        public async Task<IActionResult> Create(GetEmployeeDto employeeDto)
        {

            if (ModelState.IsValid)
            {
                await _employeeService.AddEmployee(employeeDto);
                return RedirectToAction("Index");
            }
            return View(employeeDto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var Emp = await _employeeService.GetByIdEmployee(id);
            if (Emp == null)
            {
                return NotFound();
            }
            return View(Emp);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GetEmployeeDto employeeDto)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.UpdateEmployee(employeeDto);
                return RedirectToAction("Index");
            }
            return View(employeeDto);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var Emp = await _employeeService.GetByIdEmployee(id);
            if (Emp == null)
            {
                return NotFound();
            }
            _employeeService.DeleteEmployee(Emp);
            return RedirectToAction("Index");
        }
    }
}
