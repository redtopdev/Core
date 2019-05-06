using System.Threading.Tasks;

namespace Engaze.Core.MessageBroker.Consumer
{
    public interface IMessageHandler
    {
        Task OnMessageReceivedAsync(string message);
        void  OnError(string error);
    }
}
