using SGM.Legado.STUR.Core.Mensagens;
using System;
using System.Threading.Tasks;

namespace SGM.Legado.STUR.Core.MessageBus
{
	public interface IMessageBus : IDisposable
	{
		#region Padrão Publish/Subscribe

		Task PublishAsync<T>(T message) where T : IntegrationEvent;

		void SubscribeAsync<T>(Action<T> onMessage) where T : class;

		#endregion
	}
}
