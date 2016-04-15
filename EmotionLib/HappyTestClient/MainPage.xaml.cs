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


using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HappyTestClient
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string[] Person = new string[5];
        private string[] Store = new string[4];
        private int PersonIndex = 0;
        private int StoreIndex = 0;

        public MainPage()
        {
            this.InitializeComponent();

            InitPersons();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 5);
            timer.Start();

        }

        private async void Timer_Tick(object sender, object e)
        {
            //Image setting
            Image img = sender as Image;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri(Person[PersonIndex]);

            imgPerson.Source = bitmapImage;

            //emotion api calling
            EmotionClient client = new EmotionClient();
            Scores scores = await client.RecognizeAsync(Person[PersonIndex]);
            HappyModel model = new HappyModel(Store[StoreIndex], 10, 20, scores);

            DisplayModel(model);

            //IoTHubProxy data sending
            IoTHubProxy proxy = new IoTHubProxy();
            proxy.SendMessage(model);


            PersonIndex++;
            StoreIndex++;


            //Init index
            if (PersonIndex == Person.Count() - 1) PersonIndex = 0;
            if (StoreIndex == Store.Count() - 1) StoreIndex = 0;
        }

        private void InitPersons()
        {
            Person[0] = "http://i.imgur.com/OipSklR.jpg";
            Person[1] = "http://4.bp.blogspot.com/-gtvjiFZcQHE/Tx4XCeKa8oI/AAAAAAAABOM/_iLbmIPCOyo/s1600/barack-obama.jpg";
            Person[2] = "http://upload.enews24.net/News/NewsThumbnail/20160308/91887730.jpg";
            Person[3] = "http://cfile22.uf.tistory.com/image/177FEE4F508F91132390FF";
            Person[4] = "https://pbs.twimg.com/media/CbA6qThVAAEpmLy.jpg";
        }

        private void InitStore()
        {
            Store[0] = "Seoul";
            Store[1] = "Singapore";
            Store[2] = "Manila";
            Store[3] = "Tokyo";
        }


        private void DisplayModel(HappyModel model)
        {
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
        }

    }
}
