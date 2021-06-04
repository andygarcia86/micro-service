using System.Threading.Tasks;
using MassTransit;
using MS.Contracts;

namespace MS.Consumer.Consumer
{
    public class OrderConsumer :
       IConsumer<Order>
    {
        public async Task Consume(ConsumeContext<Order> context)
        {
            var data = context.Message;
            // message received.
        }
    }
}
