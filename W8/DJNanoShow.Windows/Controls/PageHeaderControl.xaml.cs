using System.Net.NetworkInformation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AppStudio.Common.Navigation;
using AppStudio.Common.Services;

namespace DJNanoShow.Controls
{
    public enum PageHeaderType { Logo, LogoText, Text, None };

    public sealed partial class PageHeaderControl : UserControl
    {
        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(PageHeaderControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty LastUpdateDateTimeProperty =
            DependencyProperty.Register("LastUpdateDateTime", typeof(string), typeof(PageHeaderControl), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ShowBackButtonProperty =
            DependencyProperty.Register("ShowBackButton", typeof(bool), typeof(PageHeaderControl), new PropertyMetadata(false));

        public static readonly DependencyProperty PageHeaderTypeProperty =
            DependencyProperty.Register("PageHeaderType", typeof(PageHeaderType), typeof(PageHeaderControl), new PropertyMetadata(PageHeaderType.None));

        public static readonly DependencyProperty ShowSnappedModeProperty =
            DependencyProperty.Register("ShowSnappedMode", typeof(bool), typeof(PageHeaderControl), new PropertyMetadata(false));

        public static readonly DependencyProperty HasLocalDataProperty =
            DependencyProperty.Register("HasLocalData", typeof(bool), typeof(PageHeaderControl), new PropertyMetadata(false));

        public PageHeaderControl()
        {
            this.InitializeComponent();
        }

        public string HeaderText
        {
            get { return (string)GetValue(HeaderTextProperty); }
            set { SetValue(HeaderTextProperty, value); }
        }

        public string LastUpdateDateTime
        {
            get { return (string)GetValue(LastUpdateDateTimeProperty); }
            set { SetValue(LastUpdateDateTimeProperty, value); }
        }

        public bool ShowBackButton
        {
            get { return (bool)GetValue(ShowBackButtonProperty); }
            set { SetValue(ShowBackButtonProperty, value); }
        }

        public PageHeaderType PageHeaderType
        {
            get { return (PageHeaderType)GetValue(PageHeaderTypeProperty); }
            set { SetValue(PageHeaderTypeProperty, value); }
        }

        public bool ShowSnappedMode
        {
            get { return (bool)GetValue(ShowSnappedModeProperty); }
            set { SetValue(ShowSnappedModeProperty, value); }
        }

        public bool HasLocalData
        {
            get { return (bool)GetValue(HasLocalDataProperty); }
            set { SetValue(HasLocalDataProperty, value); }
        }

        public bool IsNetworkAvailable
        {
            get { return InternetConnection.IsInternetAvailable(); }
        }


        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
