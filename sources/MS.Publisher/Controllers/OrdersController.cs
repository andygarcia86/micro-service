using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MS.Contracts;
namespace MS.Publisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        readonly IPublishEndpoint _publishEndpoint;
        public OrdersController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        [HttpPost]
        public async Task<ActionResult> Post(Order order)
        {
            await _publishEndpoint.Publish<Order>(order);
            return Ok("Success");
        }
    }
}