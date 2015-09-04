using System.Net.NetworkInformation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AppStudio.Common.Services;

namespace DJNanoShow.Controls
{
    public sealed partial class ConnectionNotificationControl : UserControl
    {
        public static readonly DependencyProperty LastUpdateDateTimeProperty =
            DependencyProperty.Register("LastUpdateDateTime", typeof(string), typeof(ConnectionNotificationControl), new PropertyMetadata(string.Empty));
        
        public ConnectionNotificationControl()
        {
            this.InitializeComponent();
        }

        public string LastUpdateDateTime
        {
            get { return (string)GetValue(LastUpdateDateTimeProperty); }
            set { SetValue(LastUpdateDateTimeProperty, value); }
        }

        public bool IsNetworkAvailable
        {
            get { return InternetConnection.IsInternetAvailable(); }
        }

    }
}
