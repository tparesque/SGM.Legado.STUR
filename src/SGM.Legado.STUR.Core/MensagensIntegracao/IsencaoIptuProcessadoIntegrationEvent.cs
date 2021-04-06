using SGM.Legado.STUR.Core.Mensagens;
using System;

namespace SGM.Legado.STUR.Core.MensagensIntegracao
{
	public class IsencaoIptuProcessadoIntegrationEvent : IntegrationEvent
	{
		public string MatriculaImovel { get; private set; }
		public string Justificativa { get; private set; }
		public SituacaoSolicitacaoEnum SituacaoSolicitacao { get; private set; }

		public string UsuarioNome { get; private set; }
		public Guid UsuarioId { get; private set; }

		public IsencaoIptuProcessadoIntegrationEvent(Guid solicitacaoId, string matriculaImovel, string justificativa, SituacaoSolicitacaoEnum situacaoSolicitacao, string usuarioNome, Guid usuarioId)
		{
			base.AggregateId = solicitacaoId;
			MatriculaImovel = matriculaImovel;
			Justificativa = justificativa;
			SituacaoSolicitacao = situacaoSolicitacao;
			UsuarioNome = usuarioNome;
			UsuarioId = usuarioId;
		}
	}

	public enum SituacaoSolicitacaoEnum
	{
		NaoInformado,
		Deferido,
		Indeferido
	}
}
