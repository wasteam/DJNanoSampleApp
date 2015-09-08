using DJNanoShow.Services;
using System;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DJNanoShow.Layouts
{
    public sealed partial class YouTubeDetailLayout : BaseDetailLayout
    {
        public YouTubeDetailLayout()
        {
            InitializeComponent();
            if (!Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                DisplayInformation.GetForCurrentView().OrientationChanged += ((sender, args) =>
                {
                    UpdateTrigger(sender.CurrentOrientation);
                });
            }
        }
        private void UpdateTrigger(Windows.Graphics.Display.DisplayOrientations orientation)
        {
            var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
            var isOnMobile = qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"].ToLowerInvariant() == "Mobile".ToLowerInvariant();
            if (orientation == Windows.Graphics.Display.DisplayOrientations.Landscape ||
               orientation == Windows.Graphics.Display.DisplayOrientations.LandscapeFlipped)
            {
                if (isOnMobile)
                {
                    FullScreenService.ChangeFullScreenMode();
                }
            }
            else if (orientation == Windows.Graphics.Display.DisplayOrientations.Portrait ||
                    orientation == Windows.Graphics.Display.DisplayOrientations.PortraitFlipped)
            {
                if (isOnMobile)
                {
                    FullScreenService.ChangeFullScreenMode();
                }
            }
        }
        private void WebView_Unloaded(object sender, RoutedEventArgs e)
        {
            WebView webView = sender as WebView;
            if (webView != null) webView.NavigateToString(string.Empty);
        }
        public override void UpdateFontSize()
        {
        }

        private void DisableFullScreenClick(object sender, RoutedEventArgs e)
        {
            try
            {
                FullScreenService.ChangeFullScreenMode();
            }
            catch (Exception)
            {
            }
        }
    }
}
