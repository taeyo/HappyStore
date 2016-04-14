using Microsoft.ProjectOxford.Emotion.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Happy;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HappyTestClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            EmotionClient client = new EmotionClient();
            Scores scores = await client.RecognizeAsync("http://hydroday.com/wp-content/uploads/2014/10/This-man-thinks-his-spotted.jpg");
            HappyModel model = new HappyModel("store1", 10, 20, scores);

            // test function
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("storeID : {0}\n", model.storeID);
            sb.AppendFormat("time : {0}\n", model.time);
            sb.AppendFormat("Anger : {0}\n", model.Anger);
            sb.AppendFormat("Contempt : {0}\n", model.Contempt);
            sb.AppendFormat("Disgust : {0}\n", model.Disgust);
            sb.AppendFormat("Fear : {0}\n", model.Fear);
            sb.AppendFormat("Happiness : {0}\n", model.Happiness);
            sb.AppendFormat("Neutral : {0}\n", model.Neutral);
            sb.AppendFormat("Sadness : {0}\n", model.Sadness);
            sb.AppendFormat("Surprise : {0}\n", model.Surprise);
            textbox.Text = sb.ToString();

            IoTHubProxy proxy = new IoTHubProxy();
            proxy.SendMessage(model);
        }

    }
}
