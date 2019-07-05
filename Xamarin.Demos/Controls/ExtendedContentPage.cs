using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace Xamarin.Demos.Controls
{
    public class ExtendedContentPage : ContentPage
    {
        private double _width = 0;
        private double _height = 0;
        private bool _safeAreaWasSet;
        private bool _firstLoad;

        public static readonly BindableProperty StartColorProperty = BindableProperty.Create("StartColor", typeof(Color), typeof(ExtendedContentPage), default(Color), BindingMode.TwoWay);
        public static readonly BindableProperty EndColorProperty = BindableProperty.Create("EndColor", typeof(Color), typeof(ExtendedContentPage), default(Color), BindingMode.TwoWay);

        public Color StartColor
        {
            get => (Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }

        public Color EndColor
        {
            get => (Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!_safeAreaWasSet)
            {
                SetSafeArea();
                _safeAreaWasSet = true;
            }

            if (_firstLoad)
                return;

            _firstLoad = true;
            OnFirstAppearing();
        }

        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height); //Must be called.

        //    if (!(Math.Abs(_width - width) > double.Epsilon) && !(Math.Abs(_height - height) > double.Epsilon))
        //        return;

        //    _width = width;
        //    _height = height;

        //    //Reset safe area on rotation as the landscape/portrait safe areas are different.
        //    SetSafeArea();
        //}

        /// <summary>
        /// This method is called when it's the first time that the view appeared.
        /// </summary>
        protected virtual void OnFirstAppearing()
        { }


        /// <summary>
        /// Sets the safe area of the device.
        /// </summary>
        private void SetSafeArea()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                //Content.Margin = DependencyService.Get<IThemeHelper>().GetBarSize();
                return;
            }

            //Set safe insets on content that you want to be within the safe area.
            var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();

            //When the safe area is empty, it means that the app is running on a non X device.
            if (Math.Abs(safeInsets.VerticalThickness) < double.Epsilon)
                safeInsets.Top += 20; //Non X devices don't take into consideration the status bar.

            Content.Margin = safeInsets;
        }
    }
}