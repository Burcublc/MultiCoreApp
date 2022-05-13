using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiCoreApp.MVC.ApiServices;
using MultiCoreApp.MVC.DTOs;

namespace MultiCoreApp.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;
        private readonly IMapper _mapper;
        public ProductController(ProductApiService productApiService,IMapper mapper,CategoryApiService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
            _mapper = mapper;  
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductWithCategoryDto> pro = await _productApiService.GetAllWithCategoryAsync();
            return View(pro);
        }
        public IActionResult Create()
        {
            var cat = _categoryApiService.GetAllAsync().Result;
            ViewData["CategoryId"] = new SelectList(cat,"Id","Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductWithCategoryDto pro)
        {
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                await _productApiService.AddAsync(pro);
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_categoryApiService.GetAllAsync().Result, "Id", "Name",pro.CategoryId);
            return View(pro);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var proDto = await _productApiService.GetById(id);
            ViewData["CategoryId"] = new SelectList(_categoryApiService.GetAllAsync().Result, "Id", "Name", proDto.CategoryId);
            return View(proDto);

        }
        [HttpPost]
        public async Task<IActionResult> Update(ProductWithCategoryDto proDto)
        {
            ModelState.Remove("Category");//burda category sınıfını güncellememesi için Category 'i devre dışı bıraktımn
            if (ModelState.IsValid)
            {
                await _productApiService.Update(proDto);
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_categoryApiService.GetAllAsync().Result, "Id", "Name", proDto.CategoryId);
            return View(proDto);
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            var proDto = await _productApiService.GetByIdForDetail(id);
            return View(_mapper.Map<ProductWithCategoryDto>(proDto));
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            return View();
        }
    }
}
