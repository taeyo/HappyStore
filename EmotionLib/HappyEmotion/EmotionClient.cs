using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.Diagnostics.Contracts;
using System.IO;

namespace Happy
{
    public class EmotionClient
    {
        private const string subscriptionKey = "4ec9affa4cac4b45be856ad273d51e31";
        private EmotionServiceClient client;
        public EmotionClient()
        {
            client = new EmotionServiceClient(subscriptionKey);
        }

        public async Task<HappyModel> RecognizeAsync(string storeID, String imageUrl)
        {
            Emotion[] emotions = await client.RecognizeAsync(imageUrl);

            // Use first emotion data
            return new HappyModel(storeID, emotions[0]);
        }
        
        public async Task<HappyModel> RecognizeAsync(string storeID, Stream imageStream)
        {
            Emotion[] emotions = await client.RecognizeAsync(imageStream);

            // Use first emotion data
            return new HappyModel(storeID, emotions[0]);
        }


    }
}
