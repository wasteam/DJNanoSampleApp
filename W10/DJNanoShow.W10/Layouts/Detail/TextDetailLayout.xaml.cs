using AppStudio.Controls;
using Windows.UI.Xaml.Controls;

namespace DJNanoShow.Layouts
{
    public sealed partial class TextDetailLayout : BaseDetailLayout
    {
        public TextDetailLayout()
        {
            InitializeComponent();
        }

        public override async void UpdateFontSize()
        {
            if (mainFlip != null && mainFlip.SelectedIndex != -1)
            {
                var container = mainFlip.ContainerFromItem(mainFlip.Items[mainFlip.SelectedIndex]);
                if (container != null)
                {
                    var children = AllChildren(container);
                    if (children != null)
                    {
                        var readingWebView = children.Find(x => x.Name.Equals("readingWebView")) as ReadingWebView;
                        await readingWebView?.TryApplyFontSizes(BodyFontSize);
                    }
                }
            }            
        }

        private async void readingWebView_ReadingWebViewNavigationCompleted(object sender, ReadingWebViewNavigationCompletedEventArgs args)
        {
            var readingWebView = sender as ReadingWebView;
            await readingWebView?.TryApplyFontSizes(BodyFontSize);
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateFontSize();
        }
    }
}
