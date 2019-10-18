using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Navigation.ViewModel;
using Plugin.Fingerprint;

namespace Navigation.View
{
    public partial class MainPage : ContentPage
    {
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
                    await DisplayAlert("Failed", "Fingerprint authentication failed, please try again.", "OK");
            }
        }
    }
}