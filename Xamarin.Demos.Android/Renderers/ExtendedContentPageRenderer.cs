using System.ComponentModel;
using Android.Content;
using Xamarin.Demos.Controls;
using Xamarin.Demos.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedContentPage), typeof(ExtendedContentPageRenderer))]
namespace Xamarin.Demos.Droid.Renderers
{
    public class ExtendedContentPageRenderer : PageRenderer
    {
        private Color StartColor { get; set; }
        private Color EndColor { get; set; }

        public ExtendedContentPageRenderer(Context context) : base(context)
        { }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            global::Android.Util.Log.WriteLine(global::Android.Util.LogPriority.Debug, "MyApp", e.PropertyName);

            //Here!
            if (e.PropertyName == ExtendedContentPage.StartColorProperty.PropertyName ||
                e.PropertyName == ExtendedContentPage.EndColorProperty.PropertyName)
            {
                var page = sender as ExtendedContentPage;

                if (page == null)
                    return;

                StartColor = page.StartColor;
                EndColor = page.EndColor;

                Invalidate();
                RefreshDrawableState();
            }
        }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            var gradient = new global::Android.Graphics.LinearGradient(0, 0, Width, 0,
                StartColor.ToAndroid(), EndColor.ToAndroid(),
                global::Android.Graphics.Shader.TileMode.Mirror);

            var paint = new global::Android.Graphics.Paint
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);

            base.DispatchDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            if (!(e.NewElement is ExtendedContentPage page))
                return;

            StartColor = page.StartColor;
            EndColor = page.EndColor;
        }
    }
}