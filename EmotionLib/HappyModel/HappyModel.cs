using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;

namespace Happy
{
    public class HappyModel : Scores
    {
        public HappyModel(string storeID, Emotion emotion)
        {
            this.storeID = storeID;
            this.time = DateTime.UtcNow;

            this.Anger = emotion.Scores.Anger;
            this.Contempt = emotion.Scores.Contempt;
            this.Disgust = emotion.Scores.Disgust;
            this.Fear = emotion.Scores.Fear;
            this.Happiness = emotion.Scores.Happiness;
            this.Neutral = emotion.Scores.Neutral;
            this.Sadness = emotion.Scores.Sadness;
            this.Surprise = emotion.Scores.Surprise;
        }

        public string storeID { get; private set; }
        public DateTime time { get; private set; }
    }
}
