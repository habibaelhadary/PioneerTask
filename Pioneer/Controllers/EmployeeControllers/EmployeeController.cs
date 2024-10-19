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
        //[HttpPost]
        //public async Task<IActionResult> Create(GetEmployeeDto employeeDto)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        await _employeeService.AddEmployee(employeeDto);
        //        return RedirectToAction("Index");
        //    }
        //    return View(employeeDto);
        //}
        [HttpPost]
        public async Task<IActionResult> Create(IFormCollection form)
        {
         
            var selectedProperties = new List<EmployeePropertiesDto>();

            var propertyIds = form["SelectedProperties_PropId"];
            var propertyValues = form["SelectedProperties_Value"];

            for (int i = 0; i < propertyIds.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(propertyValues[i]))
                {
                    selectedProperties.Add(new EmployeePropertiesDto
                    {
                        PropId = int.Parse(propertyIds[i]),
                        Value = propertyValues[i]
                    });
                }
            }

            // Create the GetEmployeeDto object from form data
            var employeeDto = new GetEmployeeDto
            {
                EmployeeCode = form["EmployeeCode"],
                EmployeeName = form["EmployeeName"],
                SelectedProperties = selectedProperties
            };

            if (ModelState.IsValid)
            {
              
                await _employeeService.AddEmployee(employeeDto);
                return RedirectToAction("Index");
            }

       
            employeeDto.Properties = await _propertyService.GetAllProperty();
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

        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var Emp = await _employeeService.GetByIdEmployee(id);
            if (Emp == null)
            {
                return NotFound();
            }
          await    _employeeService.DeleteEmployee(Emp);
            return RedirectToAction("Index");
        }
    }
}
