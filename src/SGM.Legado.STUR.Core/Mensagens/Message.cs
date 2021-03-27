using FluentValidation.Results;
using System;

namespace SGM.Legado.STUR.Core.Mensagens
{
    public abstract class Message
    {
        public string MessageType { get; set; }
        public Guid AggregateId { get; set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }

    public class Event : Message//, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }

    public abstract class IntegrationEvent : Event
    {

    }

    public class ResponseMessage : Message
    {
        public ValidationResult ValidationResult { get; set; }

        public ResponseMessage(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }



}
