using System.IO;
using Xamarin.Demos.Controls;
using Xamarin.Forms;

namespace Xamarin.Demos.ViewModel
{
    public class Image : BindableBase
    {
        private long _id;
        private int _position;
        private byte[] _content;

        public long Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public int Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        public byte[] Content
        {
            get => _content;
            set
            {
                SetProperty(ref _content, value);
                OnPropertyChanged(nameof(ImageSource));
                OnPropertyChanged(nameof(HasImage));
            }
        }

        public Xamarin.Forms.ImageSource ImageSource => Content != null ? ImageSource.FromStream(() => new MemoryStream(Content)) : null;

        public bool HasImage => !(Content == null || Content.Length == 0);
    }
}