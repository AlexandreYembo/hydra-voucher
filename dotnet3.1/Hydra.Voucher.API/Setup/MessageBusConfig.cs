using Hydra.Core.Extensions;
using Hydra.Core.MessageBus;
using Hydra.Voucher.API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Voucher.API.Setup
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
             //DI for Services
            //Important: It works as a singleton, and once you have a single DI, you cannot inject anything different inside, such addScoped, because context cannot be singleton.
            //Or you injection should be singleton, but this approach is not good for the application.
            //AddHostedService is a single and works in the NET Pipeline.

            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<VoucherValidationHandler>();
        }
    }
}