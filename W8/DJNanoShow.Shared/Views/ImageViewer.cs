using System;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using DJNanoShow;

namespace DJNanoShow.Controls
{
    public sealed partial class ImageViewer : PageBase
    {
        public ImageViewer() : base(true)
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string imageSource = e.Parameter as String;
            SetImageSource(imageSource);
        }

        protected override void LoadState(object navParameter)
        {
        }

        private void SetImageSource(string imageSource)
        {
            if (!String.IsNullOrEmpty(imageSource))
            {
                Uri uri = new Uri(imageSource, UriKind.RelativeOrAbsolute);
                if (!uri.IsAbsoluteUri)
                {
                    uri = new Uri(new Uri("ms-appx://"), imageSource);
                }
                image.Source = new BitmapImage(uri)
                {
                    CreateOptions = BitmapCreateOptions.None,
                    //DecodePixelHeight = 1024
                };
            }
        }
    }
}
