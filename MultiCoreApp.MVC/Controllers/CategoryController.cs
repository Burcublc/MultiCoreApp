using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MultiCoreApp.Core.IntService;
using MultiCoreApp.MVC.ApiServices;
using MultiCoreApp.MVC.DTOs;

namespace MultiCoreApp.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly CategoryApiService _categoryApiService;
        private readonly IMapper _mapper;
      
        public CategoryController(ICategoryService categoryService ,IMapper mapper,CategoryApiService categoryApiService)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            //var categories = await _categoryService.GetAllAsync();
            //return View(_mapper.Map<IEnumerable<CategoryDto>>(categories));

            //önce solutionda sağ tıkla propertyse git ordada çıkan sayfada multiple startup project kısmında mvc ve api kısmında none olana değerleri start yap!! 
            IEnumerable<CategoryDto> cat=await _categoryApiService.GetAllAsync();
            return View(cat);
        }
        public async Task<IActionResult> Detail(Guid id)
        {
            var catDto = await _categoryApiService.GetById(id);
            return View(_mapper.Map<CategoryDto>(catDto));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            await _categoryApiService.AddAsync(categoryDto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var cat = await _categoryApiService.GetById(id);
            return View(cat);
            
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto catDto)
        {
            await _categoryApiService.Update(catDto);
            return RedirectToAction("Index");
        }
        
    }
}
