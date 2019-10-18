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
    public partial class Page3 : ContentPage
    {
        public Page3()
        {
            InitializeComponent();
            btnAllow.Clicked += BtnAllow_Clicked;
        }

        private async void tester()
        {
            bool result = await CrossFingerprint.Current.IsAvailableAsync(true);
            if (result)
            {
                var auth = await CrossFingerprint.Current.AuthenticateAsync("Authenticate");
                if (auth.Authenticated)
                    await DisplayAlert("Authenticated", "Fingerprint authentication succeeded", "OK");
                else
                    await DisplayAlert("Failed", "Fingerprint authentication failed", "OK");
            }
        }
        private async void BtnAllow_Clicked(object sender, EventArgs e)
        {
            bool result = await CrossFingerprint.Current.IsAvailableAsync(true);
            if (result)
            {
                var auth = await CrossFingerprint.Current.AuthenticateAsync("Authenticate");
                if (auth.Authenticated)
                    await DisplayAlert("Authenticated", "Fingerprint authentication succeeded", "OK");
                else
                    await DisplayAlert("Failed", "Fingerprint authentication failed", "OK");
            }
        }
    }
}