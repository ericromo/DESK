using System;
using MLToolkit.Forms.SwipeCardView.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Navigation.ViewModel;

namespace Navigation.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();
            this.BindingContext = new Page1ViewModel();

        }

        private void OnDislikeClicked(object sender, EventArgs e)
        {

        }

        private void OnSuperLikeClicked(object sender, EventArgs e)
        {

        }

        private void OnLikeClicked(object sender, EventArgs e)
        {

        }
    }
}