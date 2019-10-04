using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Navigation.ViewModel;

namespace Navigation.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel(this.Navigation);
        }

        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Page1());
        //}

        //private async void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Page2());
        //}

        //private async void Button_Clicked_2(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Page3());
        //}
    }
}