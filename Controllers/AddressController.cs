using Microsoft.AspNetCore.Mvc;
using RedeSocial.Implementations.AddressModel.Services.Interfaces;

namespace RedeSocial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<IActionResult> ConsultAddressByCep([FromQuery] string cep)
        {
            var cepDto = await _addressService.ConsultCep(cep);

            if (cepDto == null)
                return NotFound("CEP não encontrado");

            return Ok(cepDto);
        }
    }
}
