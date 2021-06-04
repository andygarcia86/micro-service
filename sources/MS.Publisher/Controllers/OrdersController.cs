using System;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using MS.Publisher.Model;

namespace MS.Publisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IBusControl _bus;

        public OrdersController(IBusControl bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(Order order)
        {
            Uri uri = new Uri("rabbitmq://localhost/order-queue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(order);

            return Ok("Success");
        }
    }
}
