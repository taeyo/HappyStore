﻿using System;
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
        public HappyModel(string storeID, float temperature, float humidity, Scores scores)
        {
            this.storeID = storeID;
            this.time = DateTime.UtcNow;

            this.Anger = scores.Anger;
            this.Contempt = scores.Contempt;
            this.Disgust = scores.Disgust;
            this.Fear = scores.Fear;
            this.Happiness = scores.Happiness;
            this.Neutral = scores.Neutral;
            this.Sadness = scores.Sadness;
            this.Surprise = scores.Surprise;
        }

        public string storeID { get; private set; }
        public DateTime time { get; private set; }
        public float temperature { get; set; }
        public float humidity { get; set; }
    }
}
