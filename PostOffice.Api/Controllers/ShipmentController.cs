using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using PostOffice.Application.Contracts.Infrastructure;
using PostOffice.Application.Contracts.Persistence;
using PostOffice.Application.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostOffice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IPostOfficeEfUow _uow;
        private readonly IShipmentsDataService _shipmentsDataService;

        public ShipmentController(IPostOfficeEfUow uow, IShipmentsDataService shipmentsDataService)
        {
            _uow = uow;
            _shipmentsDataService = shipmentsDataService;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<ShipmentDto>> GetAll()
        {
            var dto = _shipmentsDataService.GetShipments();

            return Ok(dto);
        }


        [HttpPost("Create")]
        public async Task<ActionResult<ShipmentDto>> Post(ShipmentDto model)
        {
            var dto = await _shipmentsDataService.CreateShipment(model);

            return Ok(dto);
        }

        [HttpGet("GetShipmentById/{id}")]
        public async Task<ActionResult<ShipmentDto>> GetShipmentById(int id)
        {
            var dto = await _shipmentsDataService.GetShipmentById(id);

            return Ok(dto);
        }

        [HttpPut("FinalizeShipment/{id}")]
        public async Task<ActionResult<BagDto>> FinalizeShipment(int id, ShipmentDto model)
        {
            var dto = await _shipmentsDataService.FinalizeShipment(model, id);

            return Ok(dto);

        }
        [HttpGet("ValidateShipmentNumber/{number}")]
        public async Task<ActionResult<ModelValidateDto>> ValidateShipmentNumber(string number)
        {
            var dto = await _shipmentsDataService.ValidateShipmentNumber(number);

            return Ok(dto);
        }

    }
}
