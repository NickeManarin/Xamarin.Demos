using System;
using System.ComponentModel;
using Xamarin.Demos.Controls;
using Xamarin.Demos.ViewModel;

namespace Xamarin.Demos.Pages
{
    [DesignTimeVisible(false)]
    public partial class NavPage : ExtendedContentPage
    {
        public NavViewModel _model = new NavViewModel();

        public NavPage()
        {
            InitializeComponent();

            BindingContext = _model;

            _model.LoadCommand.Execute(null);
        }


        private void Next_Clicked(object sender, EventArgs e)
        {
            if (_model._index == 0)
            {
                _model.CurrentView = new Tags();
                _model._index++;
            }
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            if (_model._index == 0)
                await Navigation.PopModalAsync(true);
            else if (_model._index == 1)
            {
                _model.CurrentView = new Photos();
                _model._index--;
            }
        }
    }
}