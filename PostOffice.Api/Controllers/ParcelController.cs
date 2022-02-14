using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PostOffice.Application.Contracts.Infrastructure;
using PostOffice.Application.Contracts.Persistence;
using PostOffice.Application.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostOffice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase
    {
        private readonly IPostOfficeEfUow _uow;
        private readonly IShipmentsDataService _shipmentsDataService;

        public ParcelController(IPostOfficeEfUow uow, IShipmentsDataService shipmentsDataService)
        {
            _uow = uow;
            _shipmentsDataService = shipmentsDataService;
        }
       


        [HttpPost("Create")]
        public async Task<ActionResult<ParcelDto>> Post(ParcelDto model)
        {
            var dto = await _shipmentsDataService.CreateParcel(model);

            return Ok(dto);
        }

    }
}
