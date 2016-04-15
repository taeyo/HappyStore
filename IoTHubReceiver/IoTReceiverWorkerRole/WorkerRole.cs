using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;
using Microsoft.ServiceBus.Messaging;
using System.Text;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace IoTReceiverWorkerRole
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        private static string connectionString = "HostName=happyhub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=Q1DRXIi2JKxy5/dFoLPh3trk0Oz7xRQaDBhwF4ZV3Kk=";
        private static string iotHubD2cEndpoint = "messages/events";
        private static EventHubClient eventHubClient;

        public override void Run()
        {
            Trace.TraceInformation("IoTReceiverWorkerRole is running");

            try
            {
                this.RunAsync(this.cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            bool result = base.OnStart();

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotHubD2cEndpoint);
            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            Trace.TraceInformation("IoTReceiverWorkerRole has been started");
            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("IoTReceiverWorkerRole is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("IoTReceiverWorkerRole has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            // TODO: Replace the following with your own logic.
            //while (!cancellationToken.IsCancellationRequested)
            //{
            Trace.TraceInformation("Working");
            //await Task.Delay(1000);

            var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

            foreach (string partition in d2cPartitions)
            {
                ReceiveMessagesFromDeviceAsync(partition);
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
            }

            //}
        }

        private async static Task ReceiveMessagesFromDeviceAsync(string partition)
        {
            var eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver(partition, DateTime.UtcNow);
            while (true)
            {
                EventData eventData = await eventHubReceiver.ReceiveAsync();
                if (eventData == null)
                {
                    continue;
                }

                string data = Encoding.UTF8.GetString(eventData.GetBytes());
                InsertPersistentStorage(data);
                Trace.TraceInformation(string.Format("Message received. Partition: {0} Data: '{1}'", partition, data));
                
            }
        }



        private static void InsertPersistentStorage(string eventData)
        {
            HappyModel model = JsonConvert.DeserializeObject<HappyModel>(eventData);

            string connectionString = "Data Source=happy-database-svr.database.windows.net; Initial Catalog = HappyDatabase; Integrated Security = False; User ID = dxkorea; Password=P@ssw0rd; Connect Timeout = 60; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = $"INSERT INTO HappyData (storeCode, latitude, longitude, temperature, humidity, anger, contempt, disgust, fear, happiness, neutral, sadness, surprise) VALUES ("
                    + $"'{model.storeID}', 0, 0, {model.temperature}, {model.humidity}, {model.Anger}, {model.Contempt}, {model.Disgust}, {model.Fear}, {model.Happiness}, {model.Neutral}, {model.Sadness}, {model.Surprise})";
                Trace.TraceInformation(cmd.CommandText);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            
            
                
            
        }
    }
}
