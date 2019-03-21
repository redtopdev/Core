using System;
using System.Collections.Generic;
using System.Text;

namespace Engaze.Core.MessageBroker.Consumer
{
    public interface IMessageHandler
    {
        void OnMessageReceived(string message);
        void OnError(string error);
    }
}
