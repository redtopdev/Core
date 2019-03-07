
using Engaze.Core.MessageBroker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Engaze.Core.MessageBroker.Test
{
    [TestClass]
    public class MessageWriterTest
    {
        [TestMethod]
        public void WriteAsyncTest()
        {
            var config = new Dictionary<string, object>
            {
            { "bootstrap.servers", "192.168.0.104:9092" }
            };
            IMessageProducer<string> writer = new KafkaProducer<string>(null, null);

            var res = writer.WriteAsync("hello", "topic-book").Result;
        }
    }
}
