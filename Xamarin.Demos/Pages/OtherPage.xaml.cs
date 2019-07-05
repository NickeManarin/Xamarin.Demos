using System;
using System.ComponentModel;
using Xamarin.Demos.Controls;

namespace Xamarin.Demos
{
    [DesignTimeVisible(false)]
    public partial class OtherPage : ExtendedContentPage
    {
        public OtherPage()
        {
            InitializeComponent();
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }
    }
}