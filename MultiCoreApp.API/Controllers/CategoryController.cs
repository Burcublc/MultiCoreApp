using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiCoreApp.API.DTOs;
using MultiCoreApp.API.Filters;
using MultiCoreApp.Core.IntService;
using MultiCoreApp.Core.Models;

namespace MultiCoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryService _catService; //biz burda CategoryService yerine ICategoryService den çekicez bilgileri
        private IMapper _mapper;

        public CategoryController(ICategoryService catService,IMapper mapper)
        {
            _catService = catService;
            _mapper = mapper;
        }
        [HttpGet] //select işlemleri için api kullanılan keyworddur
        public async Task<IActionResult> GetAll()
        {
            var cat = await _catService.GetAllAsync();
            //return Ok(cat); //sonuc bekler ;200 mesajı döner, başarılı oldum mesajı oldum mesajı döndürür
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(cat)); //categoryDto tipinde cat'i Liste olarak Map'e gönder ordaki sonucu döndür
            //sürekli dto'yu newlememek için mapperı kullandık
            //CategoryDto dto=new CategoryDto();
            //Category cat1 = new Category();
            //dto.Name=cat1.Name;  gibi
        }
        [ServiceFilter(typeof(CategoryNotFoundFilter))] //filter sadece bu metod için çalış 
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var cat=await _catService.GetByIdAsync(Id);
            //return Ok(cat);
            return Ok(_mapper.Map<CategoryDto>(cat));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto catDto) //bir kayıt işlemi
        {
            var newCat = await _catService.AddAsync(_mapper.Map<Category>(catDto));//catDto verisini Category' e dönüştürcem.Yani Database'e kaydetmek için categorydto'dan category 'e dönüştürmem gerek
            return Created(String.Empty, _mapper.Map<CategoryDto>(newCat)); //kullanıcıyada oluşturma mesajı göndericeğim için tekrar categorydto'ya dönüştürdük
        }
        [HttpPut] //update ve delete işlemleri için
        public IActionResult Update(CategoryDto catDto)
        {
            _catService.Update(_mapper.Map<Category>(catDto));
            return NoContent();//bir içerik yollamıcam messaj olarak sadece hata kodunu döndürücek
        }
        [HttpDelete("{id:guid}")]//idyi zourunlu hale getirdik
        public IActionResult Remove(Guid id)
        {
            var cat = _catService.GetByIdAsync(id).Result; //asenkron bi yapıyı senkron yapıya dönüşmtürmek için result'ı yazdık, yani bana sadece sonucu dödür diyoruz
            _catService.Remove(cat);
            return NoContent();
        }

        //name 'e gör silme işlemi
        [HttpDelete("{name}")] 
        public IActionResult RemoveByName(string name)
        {
            //var cat = _catService.FirstOrDefaultAsync(s => s.Name == name).Result;
            //_catService.Remove(cat);
            var pro = _catService.Where(s => s.Name == name).Result; //where burda liste şeklinde veri getirebilir
            _catService.RemoveRange(pro); //bu şekildede yapılabilirdi
            return NoContent();
        }

        [HttpGet("{id:guid}/products")] //category getir ürünleriyle birlikte
        public async Task<IActionResult> GetWithProductById(Guid id)
        {
            var cat = await _catService.GetWithProductByIdAsync(id);
            return Ok(_mapper.Map<CategoryWithProductsDto>(cat));
        }
    }
}
