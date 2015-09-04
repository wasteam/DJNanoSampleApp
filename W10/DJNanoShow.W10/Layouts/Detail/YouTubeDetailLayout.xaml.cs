using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DJNanoShow.Layouts
{
    public sealed partial class YouTubeDetailLayout : BaseDetailLayout
    {
        public YouTubeDetailLayout()
        {
            InitializeComponent();
        }

        public override void UpdateFontSize()
        {
        }

        private void WebView_Unloaded(object sender, RoutedEventArgs e)
        {
            WebView webView = sender as WebView;
            if (webView != null) webView.NavigateToString(string.Empty);
        }
    }
}
