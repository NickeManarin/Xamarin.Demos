using System;
using Xamarin.Demos.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Demos.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Photos : ContentView
    {
        internal PhotosViewModel ViewModel { get; } = new PhotosViewModel();

        public Photos()
        {
            InitializeComponent();

            BindingContext = ViewModel;

            ViewModel.LoadPhotosCommand.Execute(null);
        }

        private void MainListView_ChildrenReordered(object sender, EventArgs e)
        {
            for (var i = 0; i < ViewModel.Items.Count; i++)
                ViewModel.Items[i].Position = i + 1;
        }
    }
}