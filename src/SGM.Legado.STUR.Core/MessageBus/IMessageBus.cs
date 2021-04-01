using SGM.Legado.STUR.Core.Mensagens;
using System;
using System.Threading.Tasks;

namespace SGM.Legado.STUR.Core.MessageBus
{
	public interface IMessageBus : IDisposable
	{

		#region Padrão Publish/Subscribe

		Task PublishAsync<T>(T message) where T : IntegrationEvent;

		void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class;


		#endregion

		#region Padrão Request/Response

		Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
			where TRequest : IntegrationEvent
			where TResponse : ResponseMessage;

		Task<IDisposable> RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
			where TRequest : IntegrationEvent
			where TResponse : ResponseMessage;


		#endregion
	}
}
