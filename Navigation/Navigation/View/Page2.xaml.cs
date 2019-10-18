using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Fingerprint;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Navigation.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();
            btnBlue.Clicked += BtnBlue_Clicked;
            btnGreen.Clicked += BtnGreen_Clicked1;
            btnRed.Clicked += BtnRed_Clicked;
            btnAlmond.Clicked += BtnAlmond_Clicked;
        }

        private void BtnAlmond_Clicked(object sender, EventArgs e)
        {
            // Enter code here
        }

        private async void BtnRed_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Red", "You've clicked the red button", "OK");
        }

        private async void BtnBlue_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Blue", "You've clicked the blue button", "OK");
        }

        private async void BtnGreen_Clicked1(object sender, EventArgs e)
        {
            await DisplayAlert("ERIC ROMO", "PROFESSIONAL AUTOMATON", "CASEY PLS");
        }
    }

}