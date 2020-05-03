using Confluent.Kafka;
using System;
using System.Threading.Tasks;

namespace Engaze.Core.MessageBroker
{
    public interface IMessageProducer<T> where T : class
    {
        Task<DeliveryResult<Null, string>> WriteAsync(T message, string topic);       
        void Write(T message, string topic);

    }
}
