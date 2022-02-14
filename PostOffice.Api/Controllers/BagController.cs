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
    public class BagController : ControllerBase
    {
        private readonly IPostOfficeEfUow _uow;
        private readonly IShipmentsDataService _shipmentsDataService;

        public BagController(IPostOfficeEfUow uow, IShipmentsDataService shipmentsDataService)
        {
            _uow = uow;
            _shipmentsDataService = shipmentsDataService;
        }
        
       


        [HttpPost("Create")]
        public async Task<ActionResult<BagDto>> Post(BagDto model)
        {
            var dto = await _shipmentsDataService.CreateBag(model);

            return Ok(dto);
        }
        [HttpPut("Update/{id}")]
        public async Task<ActionResult<BagDto>> UpdateBag(int id, BagDto model)
        {
            var dto = await _shipmentsDataService.UpdateBag(model,id);

            return Ok(dto);

        }

    }
}
