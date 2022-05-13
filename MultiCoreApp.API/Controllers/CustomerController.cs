using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiCoreApp.API.DTOs;
using MultiCoreApp.Core.IntRepository;
using MultiCoreApp.Core.IntService;
using MultiCoreApp.Core.IntUnitOfWork;
using MultiCoreApp.Core.Models;
using MultiCoreApp.Service.Services;

namespace MultiCoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _cus;
        IMapper _mapper;
        public CustomerController(ICustomerService cus, IMapper mapper)
        {
            _cus = cus;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customer = await _cus.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CustomerDto>>(customer));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _cus.GetByIdAsync(id);
            return Ok(_mapper.Map<CustomerDto>(customer));
        }

        [HttpPost]
        public async Task<IActionResult> Save(CustomerDto cusDto) //bir kayıt işlemi
        {
            var newCustomer= await _cus.AddAsync(_mapper.Map<Customer>(cusDto));//catDto verisini Category' e dönüştürcem.Yani Database'e kaydetmek için categorydto'dan category 'e dönüştürmem gerek
            return Created(String.Empty, _mapper.Map<CustomerDto>(newCustomer)); //kullanıcıyada oluşturma mesajı göndericeğim için tekrar categorydto'ya dönüştürdük
        }
        [HttpPut] //update ve delete işlemleri için
        public IActionResult Update(CustomerDto cusDto)
        {
            _cus.Update(_mapper.Map<Customer>(cusDto));
            return NoContent();//bir içerik yollamıcam messaj olarak sadece hata kodunu döndürücek
        }
        [HttpDelete("{id:guid}")]//idyi zourunlu hale getirdik
        public IActionResult Remove(Guid id)
        {
            var customer = _cus.GetByIdAsync(id).Result; //asenkron bi yapıyı senkron yapıya dönüşmtürmek için result'ı yazdık, yani bana sadece sonucu dödür diyoruz
            _cus.Remove(customer);
            return NoContent();
        }

        //name 'e göre silme işlemi
        [HttpDelete("{name}")]
        public IActionResult RemoveByName(string name)
        {
        
            var customer = _cus.Where(s => s.Name == name).Result; 
            _cus.RemoveRange(customer); 
            return NoContent();
        }

       
    }
}
