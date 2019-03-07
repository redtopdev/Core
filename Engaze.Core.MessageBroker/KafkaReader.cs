using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CampusPulse.Core.MessageBroker
{
    public class KafkaReader
    {
        private readonly Dictionary<string, object> config;
        public KafkaReader(Dictionary<string, object> config)
        {
            this.config = config;
            //
            this.config = new Dictionary<string, object>
            {
                { "bootstrap.servers", "host1:9092,host2:9092" },
                { "group.id", "foo" },
                { "default.topic.config", new Dictionary<string, object>
                    {
                        { "auto.offset.reset", "smallest" }
                    }
                }
            };
        }
        public void StartListning(string topic)
        {


            // Create the consumer
            /* using (var consumer = new Consumer<Null, string>(config, null, new StringDeserializer(Encoding.UTF8)))
             {
                 consumer.Subscribe(topics);
                 consumer.OnMessage += (_, msg) =>
                 {
                     processMessage(msg);
                     msgCount += 1;
                     if (msgCount % MIN_COMMIT_COUNT == 0)
                     {
                         consumer.CommitAsync().ContinueWith(
                             commitResult =>
                             {
                                 if (commitResult.Error)
                                 {
                                     Console.Error.WriteLine(commitResult.Error);
                                 }
                                 else
                                 {
                                     Console.WriteLine(
                                         $"Committed Offsets [{string.Join(", ", commitResult.Offsets)}]");
                                 }
                             }
                         )
                     }
                 }

                 consumer.OnPartitionEOF += (_, end)
                     => Console.WriteLine($"Reached end of topic {end.Topic} partition {end.Partition}.");

                 consumer.OnError += (_, error)
     {
                     Console.WriteLine($"Error: {error}");
                     cancelled = true;
                 }

                 consumer.Subscribe(topics);

                 while (!cancelled)
                 {
                     consumer.Poll(TimeSpan.FromSeconds(1));
                 }
             }
         }

         private void processMessage(Message<Null, string> msg)
         {
             throw new NotImplementedException();
         }
        }*/
        }
    }
}
