using AppStudio.Controls;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace DJNanoShow.Layouts.List
{
    public sealed partial class ListTour : ListLayoutBase
    {
        public ListTour() : base()
        {
            this.InitializeComponent();
        }
        private void OnElementHolding(object sender, Windows.UI.Xaml.Input.HoldingRoutedEventArgs args)
        {
            // this event is fired multiple times. We do not want to show the menu twice
            if (args.HoldingState != HoldingState.Started) return;

            FrameworkElement element = sender as FrameworkElement;
            if (element == null) return;

            // If the menu was attached properly, we just need to call this handy method
            FlyoutBase.ShowAttachedFlyout(element);
        }

        private void OnElementTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element == null) return;

            // If the menu was attached properly, we just need to call this handy method
            FlyoutBase.ShowAttachedFlyout(element);
        }
    }
}
