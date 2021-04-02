using Microsoft.Extensions.Hosting;
using SGM.Legado.STUR.Core.MensagensIntegracao;
using SGM.Legado.STUR.Core.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SGM.Legado.STUR.Api.Services
{
	public class SolicitacaoIsencaoIptuIntegrationHandler : BackgroundService
	{
		private readonly IMessageBus _messageBus;

		public SolicitacaoIsencaoIptuIntegrationHandler(IMessageBus messageBus)
		{
			_messageBus = messageBus;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			_messageBus.Subscribe<IsencaoIptuSolicitadoIntegrationEvent>(solicitacao => ProcessarSolicitacao(solicitacao));

			return Task.CompletedTask;
		}

		private void ProcessarSolicitacao(IsencaoIptuSolicitadoIntegrationEvent solicitacao)
		{
			SituacaoSolicitacaoEnum situacaoSolicitacao;
			string justificativa = string.Empty;

			if (DeveAprovarIsencao(solicitacao.MatriculaImovel))
			{
				situacaoSolicitacao = SituacaoSolicitacaoEnum.Deferido;
				justificativa = $"O contribuinte apresentou elementos que justificassem o deferimento da solicitação. O imóvel com a matrícula {solicitacao.MatriculaImovel} está isento de IPTU para o exercício corrente. [mock]";
			}
			else
			{
				situacaoSolicitacao = SituacaoSolicitacaoEnum.Indeferido;
				justificativa = $"O contribuinte não apresentou elementos que justificassem o deferimento da solicitação. O imóvel com a matrícula {solicitacao.MatriculaImovel} não receberá de IPTU para o exercício corrente. [mock]";
			}

			string mockUsuarioNomeQueProcessou = "Administrador";
			Guid mockUsuarioIdQueProcessou = Guid.Parse("eb438b25-c8d7-4a1f-9320-c571d5ed19b9");

			var solicitacaoProcessada = new IsencaoIptuProcessadoIntegrationEvent(
				solicitacao.AggregateId, 
				solicitacao.MatriculaImovel, 
				justificativa, 
				situacaoSolicitacao,
				mockUsuarioNomeQueProcessou,
				mockUsuarioIdQueProcessou);

			_messageBus.Publish(solicitacaoProcessada);

			Console.WriteLine("***** RabbitMQ ***** IsencaoIptuProcessadoIntegrationEvent Finalizado");
		}

		private bool DeveAprovarIsencao(string matriculaImovel)
		{
			return matriculaImovel.Length >= 5;
		}
	}
}
