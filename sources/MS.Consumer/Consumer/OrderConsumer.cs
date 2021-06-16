using System.Threading.Tasks;
using MassTransit;
using MS.Contracts;

namespace MS.Consumer.Consumer
{
    public class OrderConsumer : IConsumer<Order>
    {
        public OrderConsumer()
        { 
        }

        public async Task Consume(ConsumeContext<Order> context)
        {
            var data = context.Message;
            // message received.
        }
    }
}
