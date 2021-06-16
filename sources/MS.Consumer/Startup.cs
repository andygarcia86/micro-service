using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS.Consumer.Consumer;

namespace MS.Consumer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(massTransit =>
            {
                massTransit.AddConsumer<OrderConsumer>();

                massTransit.UsingRabbitMq((context, configurator) =>
                {
                    var host = "localhost";
                    var virtualHost = "/";
                    var usr = "guest";
                    var psw = "guest";
                    var prefetchCount = 16;
                    var messageRetryCount = 2;
                    var messageRetryInterval = 500;

                    configurator.Host(host, virtualHost, h =>
                    {
                        h.Username(usr);
                        h.Password(psw);
                    });

                    configurator.ReceiveEndpoint("order-queue", e =>
                    {
                        e.PrefetchCount = prefetchCount;
                        e.UseMessageRetry(r => r.Interval(messageRetryCount, messageRetryInterval));
                        e.ConfigureConsumer<OrderConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
