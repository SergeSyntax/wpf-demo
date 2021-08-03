using LandmarkAI.classes;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LandmarkAI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string URL = "https://southcentralus.api.cognitive.microsoft.com/customvision/v3.0/Prediction/0a5d773d-80f8-4e39-b68f-f6cb6fc6eba2/classify/iterations/Iteration1/image";
        const string PREDICTION_KEY = "63474aa864214c7faf3a509f89ab7b53";
        const string CONTENT_TYPE = "application/octet-stream";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.png; *.jpg)|*.png;*jpg;*jpeg|All files (*.*)|*.*";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if(dialog.ShowDialog()== true) {
                string fileName = dialog.FileName;
                selectedImage.Source = new BitmapImage(new Uri(fileName));

                MakePredictionAsync(fileName);
            }
            
        }

        private async void MakePredictionAsync(string fileName)
        {


            var file = File.ReadAllBytes(fileName);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Prediction-key", PREDICTION_KEY);
                using (var content = new ByteArrayContent(file))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue (CONTENT_TYPE);
                    var response = await client.PostAsync(URL, content);
                    var responseString = await response.Content.ReadAsStringAsync();


                    List<Prediction> predictions = JsonConvert.DeserializeObject<CustomVision>(responseString).Predictions;
                    predictionsListView.ItemsSource = predictions;
                }
            };

            


        }
    }
}
