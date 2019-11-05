using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Navigation.ViewModel;
using Plugin.Fingerprint;
using System.Diagnostics;
using System.Timers;
using System.Net;
using System.Net.Http;
using Navigation.Interfaces;

namespace Navigation.View
{
    public partial class MainPage : ContentPage
    {
        private static System.Timers.Timer aTimer;
        private string ipWildcard = "192.168.68.*";
        private int i = 0;
        private int temp = 0;
        private string use = "";
        public ipMAC[] arrCopy = new ipMAC[255];

        
        public MainPage()
        {
            fingerprint();
            this.BindingContext = new MainPageViewModel(this.Navigation);

        }
        private async void fingerprint()
        {
            bool result = await CrossFingerprint.Current.IsAvailableAsync(true);
            if (result)
            {
                var auth = await CrossFingerprint.Current.AuthenticateAsync("Authenticate");
                if (auth.Authenticated)
                    InitializeComponent();
                else
                {
                    await DisplayAlert("Failed", "Fingerprint authentication failed, please try again.", "OK");
                    var closer = DependencyService.Get<ICloseApplication>();
                    closer?.closeApplication();
                    
                }
            }
        }

        private void setTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(5000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void stopTimer()
        {
            aTimer.Stop();
        }

        /// <summary>
        /// Used to constantly scan the devices and compare them to the list of devices located on our DB
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Debug.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);

            // Creating array of objects
            ipMAC[] arr = InitializeArray<ipMAC>(255);

            // Clearing vars
            i = 0;
            temp = 0;


            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }))
            {
                client.BaseAddress = new Uri("http://192.168.64.2/cgi-bin/luci/command/cfg0f9944" + "/-sP%20" + ipWildcard);
                HttpResponseMessage response = client.GetAsync("").Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Result: " + result);

                while (i != -1)
                {

                    i = result.IndexOf("Nmap scan report for", i);
                    use = result.Substring(i + 21, result.IndexOf("\n", i + 22) - (i + 21));
                    if (use == "192.168.68.2") // Breaking out of loop if it identifies the router's IP (which will be inherently the last one listed
                        break;
                    arr[temp].IP = use;
                    i = result.IndexOf("MAC Address:", i);
                    arr[temp].MAC = result.Substring(i + 13, result.IndexOf(" ", i + 14) - (i + 13));
                    temp++;
                }
                temp = 0;
            }

            arrCopy = arr;

            


        }


        T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }



        protected override void OnAppearing()
        {
            setTimer();
            Debug.WriteLine("This is the main page!");
        }

        protected override void OnDisappearing()
        {
            stopTimer();
            Debug.WriteLine("Leaving the main page!");
        }

    }
}