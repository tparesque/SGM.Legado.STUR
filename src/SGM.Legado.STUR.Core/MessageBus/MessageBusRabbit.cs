using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SGM.Legado.STUR.Core.Mensagens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGM.Legado.STUR.Core.MessageBus
{
	public class MessageBusRabbit : IMessageBus
	{
		private string _hostName;
		private IConnection _connection;
		private IModel _channel;

		public MessageBusRabbit(IOptions<RabbitMqConfigurations> options)
		{
			_hostName = options.Value.HostName;

			CriarConexao();
		}

		private void _connection_ConnectionShutdown(object sender, ShutdownEventArgs e)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{

		}

		public async Task Publish<T>(T message) where T : IntegrationEvent
		{
			if (ExisteConexao() == false)
			{
				throw new Exception("Ocorreu um erro ao tentar realizar a conexão com a mensageria.");
			}
			else
			{
				string tipo = typeof(T).Name;

				_channel.QueueDeclare(queue: tipo, durable: false, exclusive: false, autoDelete: false, arguments: null);

				var json = JsonConvert.SerializeObject(message);
				var body = Encoding.UTF8.GetBytes(json);

				_channel.BasicPublish(exchange: "", routingKey: tipo, basicProperties: null, body: body);
			}
		}

		public void Subscribe<T>(Action<T> onMessage) where T : class
		{
			if (ExisteConexao() == false)
			{
				throw new Exception("Ocorreu um erro ao tentar realizar a conexão com a mensageria.");
			}
			else
			{
				string tipo = typeof(T).Name;

				_channel.QueueDeclare(queue: tipo, durable: false, exclusive: false, autoDelete: false, arguments: null);

				//stoppingToken.ThrowIfCancellationRequested();

				var consumer = new EventingBasicConsumer(_channel);
				consumer.Received += (ch, ea) =>
				{
					var content = Encoding.UTF8.GetString(ea.Body.ToArray());
					var json = JsonConvert.DeserializeObject<T>(content);

					onMessage(json);

					_channel.BasicAck(ea.DeliveryTag, false);
				};

				consumer.Shutdown += Consumer_Shutdown;
				consumer.Registered += Consumer_Registered;
				consumer.Unregistered += Consumer_Unregistered;
				consumer.ConsumerCancelled += Consumer_ConsumerCancelled;

				_channel.BasicConsume(tipo, false, consumer);

			}
		}

		private void Consumer_ConsumerCancelled(object sender, ConsumerEventArgs e)
		{

		}

		private void Consumer_Unregistered(object sender, ConsumerEventArgs e)
		{

		}

		private void Consumer_Registered(object sender, ConsumerEventArgs e)
		{

		}

		private void Consumer_Shutdown(object sender, ShutdownEventArgs e)
		{

		}

		private void CriarConexao()
		{
			try
			{
				var factory = new ConnectionFactory
				{
					HostName = _hostName
				};

				_connection = factory.CreateConnection();
				_connection.ConnectionShutdown += Consumer_Shutdown;
				_channel = _connection.CreateModel();
			}
			catch (Exception)
			{

				throw;
			}
		}

		private bool ExisteConexao()
		{
			if (_connection != null)
			{
				return true;
			}

			CriarConexao();

			return _connection != null;
		}
	}
}
