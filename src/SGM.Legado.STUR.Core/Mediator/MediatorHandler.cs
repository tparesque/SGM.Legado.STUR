using FluentValidation.Results;
using MediatR;
using SGM.Legado.STUR.Core.Mensagens;
using System.Threading.Tasks;

namespace SGM.Legado.STUR.Core.Mediator
{

	public interface IMediatorHandler
	{
		Task<ValidationResult> Send<T>(T command) where T : Command;
	}

	public class MediatorHandler : IMediatorHandler
	{
		private readonly IMediator _mediator;

		public MediatorHandler(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<ValidationResult> Send<T>(T command) where T : Command
		{
			var result = await _mediator.Send(command);

			return result;
		}
	}
}
