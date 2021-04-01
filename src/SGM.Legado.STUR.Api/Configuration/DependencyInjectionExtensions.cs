using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SGM.Legado.STUR.Core.MessageBus;
using System;

namespace SGM.Legado.STUR.Api.Configuration
{
	public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMqConfigurations>(configuration.GetSection("MessageQueueConnection"));

            services.AddSingleton<IMessageBus, MessageBusRabbit>();

            return services;
        }
    }
}
