using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Happy;

using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Threading;

namespace HappyTestClient
{
    class IoTHubProxy
    {
        private static DeviceClient deviceClient;
        private static string iotHubUri = "happyhub.azure-devices.net";
        private static string deviceKey = "N0F9+Pe+GWddFQq8QH0nvTHVwPgiKnXlVA618KImA30=";

        public IoTHubProxy()
        {
            deviceClient = DeviceClient.Create(iotHubUri, 
                new DeviceAuthenticationWithRegistrySymmetricKey("myFirstDevice", deviceKey));
        }
        public async void SendMessage(HappyModel model)
        {
            var messageString = JsonConvert.SerializeObject(model);
            var message = new Message(Encoding.ASCII.GetBytes(messageString));

            await deviceClient.SendEventAsync(message);
        }
    }
}
