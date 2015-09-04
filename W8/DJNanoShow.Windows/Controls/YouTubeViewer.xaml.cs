using System.ComponentModel;
using AppStudio.Common.Navigation;
using AppStudio.Common.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DJNanoShow.Controls
{
    public sealed partial class YouTubeViewer : UserControl, INotifyPropertyChanged
    {
        private NavigationService _navigationHelper;

        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(YouTubeViewer), new PropertyMetadata(null));

        public static readonly DependencyProperty PageTitleProperty =
            DependencyProperty.Register("PageTitle", typeof(string), typeof(YouTubeViewer), new PropertyMetadata(null));

        public static readonly DependencyProperty SummaryTextProperty =
            DependencyProperty.Register("SummaryText", typeof(string), typeof(YouTubeViewer), new PropertyMetadata(null));

        public static readonly DependencyProperty EmbedUrlProperty =
            DependencyProperty.Register("EmbedUrl", typeof(string), typeof(YouTubeViewer), new PropertyMetadata(null));
        
        public static readonly DependencyProperty ShowSnappedModeProperty =
            DependencyProperty.Register("ShowSnappedMode", typeof(bool), typeof(PageHeaderControl), new PropertyMetadata(false));

        public YouTubeViewer()
        {
            this.Unloaded += ((e, sender) => { try { this.EmbedUrl = null; } catch { } });
            this.InitializeComponent();

            DataContext = this;

            SizeChanged += OnSizeChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
        }

        public string SummaryText
        {
            get { return (string)GetValue(SummaryTextProperty); }
            set { SetValue(SummaryTextProperty, value); }
        }

        public bool ShowSnappedMode
        {
            get { return (bool)GetValue(ShowSnappedModeProperty); }
            set { SetValue(ShowSnappedModeProperty, value); }
        }

        public string EmbedUrl
        {
            get { return (string)GetValue(EmbedUrlProperty); }
            set
            {
                SetValue(EmbedUrlProperty, value);
                OnPropertyChanged("EmbedUrl");
            }
        }

        public NavigationService NavigationHelper
        {
            get
            {
                return _navigationHelper;
            }
            set
            {
                _navigationHelper = value;
                OnPropertyChanged("NavigationHelper");
            }
        }

        public bool NetworkAvailable
        {
            get { return InternetConnection.IsInternetAvailable(); }
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width < 500)
            {
                VisualStateManager.GoToState(this, "SnappedView", true);
            }
            else if (e.NewSize.Width < e.NewSize.Height)
            {
                VisualStateManager.GoToState(this, "PortraitView", true);
            }
            else
            {
                VisualStateManager.GoToState(this, "FullscreenView", true);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
