using Microsoft.Extensions.DependencyInjection;
using SGM.Legado.STUR.Core.MessageBus;
using System;

namespace SGM.Legado.STUR.Api.Configuration
{
	public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
        {
            if (string.IsNullOrEmpty(connection))
            {
                throw new ArgumentNullException();
            }

            services.AddSingleton<IMessageBus>(new MessageBus(connection));

            return services;
        }
    }
}
