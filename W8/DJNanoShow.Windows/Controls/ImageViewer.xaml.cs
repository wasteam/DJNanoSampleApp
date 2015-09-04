using AppStudio.Common.Navigation;
using Windows.UI.Xaml;

namespace DJNanoShow.Controls
{
    public sealed partial class ImageViewer : PageBase
    {
        private void OnTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            BackButton.Visibility = (BackButton.Visibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        } 
    }
}
