using Application_Layer.Dtos.Propert;
using Application_Layer.Services.Propert;
using Microsoft.AspNetCore.Mvc;

namespace Pioneer.Controllers.PropertiesControllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }
        public async Task<IActionResult> Index()
        {
            var AllProperty = await _propertyService.GetAllProperty();
            var propertyUsage = new Dictionary<int, bool>();

            foreach (var property in AllProperty)
            {
                var Use = await _propertyService.IsProperty(property.Id);
                propertyUsage.Add(property.Id,Use);
            }
            ViewBag.PropertyUsage = propertyUsage;
            return View(AllProperty);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new PropertyDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create(PropertyDto dto)
        {

            if (ModelState.IsValid)
            {
                await _propertyService.AddProperty(dto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var property = await _propertyService.GetByIdProperty(id);
            if (property == null)
            {
                return NotFound();
            }
            bool isInUse = await _propertyService.IsProperty(id);

            ViewBag.IsInUse = isInUse;
            return View(property);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PropertyDto propertyDto)
        {
            if (ModelState.IsValid)
            {
                await _propertyService.UpdateProperty(propertyDto);
                return RedirectToAction("Index");
            }
            return View(propertyDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            //var property = await _propertyService.GetByIdProperty(id);
            //if (property == null)
            //{
            //    return NotFound();
            //}

      
          await  _propertyService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
