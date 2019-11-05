using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Fingerprint;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using System.Net.Http;
using System.Collections;
using System.Diagnostics;
using System.Timers;
using Navigation.ViewModel;

namespace Navigation.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        private static System.Timers.Timer aTimer;
        private string ipWildcard = "192.168.68.*";
        private int i = 0;
        private int temp = 0;
        private string tempDevices = "";
        private string devices = "";
        private string use = "";
        private string[] arrS = new string[255];
        public ipMAC[] arrCopy = new ipMAC[255];
        private bool flag = true;

        public Page2()
        {

            InitializeComponent();
            btnBlue.Clicked += BtnBlue_Clicked;
            btnGreen.Clicked += BtnGreen_Clicked1;
            btnRed.Clicked += BtnRed_Clicked;
            btnAlmond.Clicked += BtnAlmond_Clicked;

        }

        private async void BtnAlmond_Clicked(object sender, EventArgs e)
        {

            editTest.Text = tempDevices;
            editTest.BackgroundColor = Xamarin.Forms.Color.BlanchedAlmond;
            myDevice.Text = tempDevices;


        }

        private async void BtnRed_Clicked(object sender, EventArgs e)
        {
            //editTest.Text = "You have clicked the red button\nCongratulations Eric Romo.\n" +
            //    "You have won a billion dollars, but you cannot claim it until you finish this project";
            //editTest.BackgroundColor = Xamarin.Forms.Color.Crimson;

            //bool answer = await DisplayAlert("Alert", "New device detected", "Accept", "Deny");
            //Debug.WriteLine("Answer: " + answer);

            await DisplayActionSheet("New device conncted:", "Ignore", null, "Accept", "Deny");

        }

        private async void BtnBlue_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Blue", "You've clicked the blue button", "OK");
        }

        private async void BtnGreen_Clicked1(object sender, EventArgs e)
        {
            await DisplayAlert("ERIC ROMO", "PROFESSIONAL AUTOMATON", "CASEY PLS");
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

                // TODO
                // Compare arr[temp] with the list of devices located on DB. Grab each devices from DB and search for it on the device
                // if not found, prompt the app
                // if found, do nothing



                while (arr[temp].IP != "0.0.0.0")
                {
                    arrS[temp] = "Device # " + (temp + 1) + "\n" +
                        arr[temp].IP + "\n" +
                        arr[temp].MAC;

                    temp++;
                }
                tempDevices = string.Join("\n\n", arrS.Where(s => !string.IsNullOrEmpty(s)));

                // Clearing string array
                Array.Clear(arrS, 0, arrS.Length);
            }

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
            Debug.WriteLine("This is the button page!");
        }

        protected override void OnDisappearing()
        {
            stopTimer();
            Debug.WriteLine("Leaving the button page!");
        }

    }

}
