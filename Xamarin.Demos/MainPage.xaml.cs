using System;
using System.ComponentModel;
using Xamarin.Demos.Controls;
using Xamarin.Forms;

namespace Xamarin.Demos
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ExtendedContentPage
    {
        private bool _isLight = true;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void FontTest_Clicked(object sender, EventArgs e)
        {
            var nav = new NavigationPage(new OtherPage());
            NavigationPage.SetHasNavigationBar(nav, false);

            await Navigation.PushModalAsync(nav);
        }

        private void DynamicResource_Clicked(object sender, EventArgs e)
        {
            _isLight = !_isLight;
            MainLabel.Text = $"The theme should be {ThemeHelper.ThemeIdToName(_isLight ? 0 : 1)}";

            //0: light
            //1: dark
            ThemeHelper.SelectTheme(_isLight ? 0 : 1);
        }

        private async void DynamicResourceOther_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushModalAsync(new NavigationPage(new OtherPage()));
        }
    }
}