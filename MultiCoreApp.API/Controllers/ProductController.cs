using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiCoreApp.API.DTOs;
using MultiCoreApp.API.Filters;
using MultiCoreApp.Core.IntService;
using MultiCoreApp.Core.Models;
using System.Collections.Generic;

namespace MultiCoreApp.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _proService;
        private IMapper _mapper;

        public ProductController(IProductService proService,IMapper mapper)
        {
            //dependency injection
            _proService = proService;
            _mapper = mapper;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            var pro = await _proService.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(pro));
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var pro = await _proService.GetByIdAsync(id);
            return Ok(_mapper.Map<ProductDto>(pro));
        }
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto proDto)
        {
            var newPro = await _proService.AddAsync(_mapper.Map<Product>(proDto));
            return Created(String.Empty, _mapper.Map<ProductDto>(newPro));
        }

        [HttpPut] 
        public IActionResult Update(ProductDto proDto)
        {
            _proService.Update(_mapper.Map<Product>(proDto));
            return NoContent();//bir içerik yollamıcam messaj olarak sadece hata kodunu döndürücek
        }
        [HttpDelete("{id:guid}")]//idyi zourunlu hale getirdik
        public IActionResult Remove(Guid id)
        {
            var cat = _proService.GetByIdAsync(id).Result; //asenkron bi yapıyı senkron yapıya dönüşmtürmek için result'ı yazdık, yani bana sadece sonucu dödür diyoruz
            _proService.Remove(cat);
            return NoContent();
        }

        [HttpGet("{id:guid}/category")] //category getir ürünleriyle birlikte
        public async Task<IActionResult> GetWithCategoryById(Guid id)
        {
            var pro = await _proService.GetWithCategoryByIdAsync(id);
            return Ok(_mapper.Map<ProductWithCategoryDto>(pro));
        }
        [HttpGet("categoryall")]
        public async Task<IActionResult> GetAllWithCategory()
        {
            var pro=await _proService.GetAllWithCategoryAsync();
            return Ok(_mapper.Map<IEnumerable<ProductWithCategoryDto>>(pro));
        }
    }
}
