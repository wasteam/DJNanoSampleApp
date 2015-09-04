using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DJNanoShow.Controls
{
    public sealed partial class ErrorNotificationControl : UserControl
    {
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(ErrorNotificationControl), new PropertyMetadata(false));

        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        public ErrorNotificationControl()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IsVisible = false;
        }
    }
}
