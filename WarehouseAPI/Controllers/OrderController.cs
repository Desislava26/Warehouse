using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.Contracts;
using Warehouse.Core.Models;

namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;

        public OrderController(IOrderService _service)
        {
            service = _service;
        }
        /// <summary>
        /// Клиентски номер
        /// <summary>
        /// <param name="order">Vashata poruchka</param>
        /// <returns> </returns>

        [HttpPost]
        [Route("place")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PlaceOrder(CustomerOrder order)
        {
            try
            {
               await service.PlaceOrder(order);
            }
            catch (ArgumentException ae)
            {
                return BadRequest(ae.Message);
            }
            return Ok();
        }
    }
}
