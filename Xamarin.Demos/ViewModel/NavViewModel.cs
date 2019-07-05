using Xamarin.Demos.Controls;
using Xamarin.Demos.Pages;
using Xamarin.Forms;

namespace Xamarin.Demos.ViewModel
{
    public class NavViewModel : BindableBase
    {
        public int _index = 0;

        private ContentView _currentView;

        public Command LoadCommand { get; set; }

        public ContentView CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public NavViewModel()
        {
            LoadCommand = new Command(Load_Executed);
        }

        private void Load_Executed()
        {
            CurrentView = new Photos();
        }
    }
}