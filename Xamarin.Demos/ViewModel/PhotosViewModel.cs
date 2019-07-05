using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Demos.Controls;
using Xamarin.Forms;

namespace Xamarin.Demos.ViewModel
{
    public class PhotosViewModel : BindableBase
    {
        private ObservableCollection<Image> _items = new ObservableCollection<Image>();

        public ObservableCollection<Image> Items
        {
            get => _items;
            set => SetProperty(ref _items, value);
        }

        public Command LoadPhotosCommand { get; set; }
        public Command SelectPhotoCommand { get; set; }
        public Command RemovePhotoCommand { get; set; }
        public Command ReorderedCommand { get; set; }

        public PhotosViewModel()
        {
            LoadPhotosCommand = new Command<List<Image>>(LoadPhotos_Executed);
            SelectPhotoCommand = new Command<Image>(async a => await SelectPhoto_Executed(a));
            RemovePhotoCommand = new Command(RemovePhoto_Executed);
            ReorderedCommand = new Command(Reordered_Executed);
        }

        private void LoadPhotos_Executed(List<Image> images)
        {
            try
            {
                if (images != null)
                    foreach (var img in images)
                        Items.Add(img);

                while (Items.Count < 6)
                    Items.Add(new Image { Position = Items.Count + 1 });
            }
            catch (Exception e)
            {
            }
        }

        private async Task SelectPhoto_Executed(Image param)
        {
            try
            {
                if (param == null)
                {
                    return;
                }

                //From camera = true.
                var fromCamera = true;

                await CrossMedia.Current.Initialize();

                var media = fromCamera == true ? await TakePhoto() : await PickPhoto();

                if (media == null)
                    return;

                var bytes = await Task.Factory.StartNew(() =>
                {
                    var stream = media.GetStream();

                    using (var br = new BinaryReader(stream))
                        return br.ReadBytes((int)stream.Length).ToArray();
                });

                if (Items[param.Position - 1].Content == null)
                {
                    //Simply adds an image to the latest position available.
                    var available = Items.FirstOrDefault(f => f.Content == null)?.Position - 1 ?? Items.Count - 1;

                    Items[available].Content = bytes;
                }
                else
                {
                    //Replaces an image.
                    Items[param.Position - 1].Content = bytes;
                }
            }
            catch (Exception e)
            {
            }
        }

        private void RemovePhoto_Executed(object param)
        {
            try
            {
                if (!(param is Image remove))
                {
                    return;
                }

                //Removes the image and adds another one at the end.
                Items.RemoveAt(remove.Position - 1);
                Items.Add(new Image { Position = Items.Count + 1 });

                //Re-index the images.
                for (var i = 0; i < Items.Count; i++)
                    Items[i].Position = i + 1;
            }
            catch (Exception e)
            {
            }
        }

        private void Reordered_Executed()
        {
            Items.CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (Items.Count < 6)
                return;

            //Re-index the images.
            for (var i = 0; i < Items.Count; i++)
                Items[i].Position = i + 1;

            Items.CollectionChanged -= Items_CollectionChanged;
        }

        private async Task<MediaFile> TakePhoto()
        {
            if (!CrossMedia.IsSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                return null;
            }

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                return null;
            }

            if (!await PermissionHelper.RequestCamera())
                return null;

            return await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "TestApp",
                SaveToAlbum = false,
                CompressionQuality = 75,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 640,
                SaveMetaData = true,
                DefaultCamera = CameraDevice.Front,
                AllowCropping = false,
                ModalPresentationStyle = MediaPickerModalPresentationStyle.OverFullScreen,
            });
        }

        private async Task<MediaFile> PickPhoto()
        {
            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                return null;
            }

            if (!await PermissionHelper.RequestMediaLibrary())
                return null;

            return await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 640,
            });
        }
    }
}