using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NsqSharp;

namespace NsqSharpClientDemo1
{
    class Program
    {
        static void Main()
        {
            // Create a new Consumer for each topic/channel
            var consumerCount = 2;
            var listC = new  List<Consumer>();
            for (var i = 0; i < consumerCount; i++)
            {
                var consumer = new Consumer("publishtest", $"channel{i}" );
                consumer.ChangeMaxInFlight(2500);
                consumer.AddHandler(new MessageHandler());
                consumer.ConnectToNsqLookupd("192.168.0.105:4161");
                listC.Add(consumer);
            }


            var exitEvent = new ManualResetEvent(false);

            Console.CancelKeyPress += (sender, eventArgs) => {
                eventArgs.Cancel = true;
                listC.ForEach(x => x.Stop());
                exitEvent.Set();
            };

            exitEvent.WaitOne();
           
            
        }
    }

    public class MessageHandler : IHandler
    {
        /// <summary>Handles a message.</summary>
        public void HandleMessage(IMessage message)
        {
            string msg = Encoding.UTF8.GetString(message.Body);
            Console.WriteLine(msg);
        }

        /// <summary>
        /// Called when a message has exceeded the specified <see cref="Config.MaxAttempts"/>.
        /// </summary>
        /// <param name="message">The failed message.</param>
        public void LogFailedMessage(IMessage message)
        {
            // Log failed messages
        }
    }
}
