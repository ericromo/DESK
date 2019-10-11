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
            this.SwipeCardView.InvokeSwipe(SwipeCardDirection.Left);

        }

        private void OnSuperLikeClicked(object sender, EventArgs e)
        {
            this.SwipeCardView.InvokeSwipe(SwipeCardDirection.Up);

        }

        private void OnLikeClicked(object sender, EventArgs e)
        {
            this.SwipeCardView.InvokeSwipe(SwipeCardDirection.Right);

        }
    }
}