using System;
using System.ComponentModel;
using AppStudio.Common.Services;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.ViewManagement;

namespace DJNanoShow.Controls
{
    public sealed partial class YouTubeViewer : UserControl, INotifyPropertyChanged
    {
        public static readonly DependencyProperty TitleTextProperty =
            DependencyProperty.Register("TitleText", typeof(string), typeof(YouTubeViewer), new PropertyMetadata(null));

        public static readonly DependencyProperty SummaryTextProperty =
            DependencyProperty.Register("SummaryText", typeof(string), typeof(YouTubeViewer), new PropertyMetadata(null));

        public static readonly DependencyProperty EmbedUrlProperty =
            DependencyProperty.Register("EmbedUrl", typeof(string), typeof(YouTubeViewer), new PropertyMetadata(null));
        
        public static readonly DependencyProperty PageTitleProperty =
            DependencyProperty.Register("PageTitle", typeof(string), typeof(YouTubeViewer), new PropertyMetadata(null));

        public static readonly DependencyProperty NeedsFullScreenProperty =
            DependencyProperty.Register("NeedsFullScreen", typeof(bool), typeof(YouTubeViewer), new PropertyMetadata(false));

        public YouTubeViewer()
        {
            this.Unloaded += ((sender, args) => this.EmbedUrl = null);
            this.InitializeComponent();

            DataContext = this;
            DisplayInformation.GetForCurrentView().OrientationChanged += this.OnOrientationChanged;
            this.TransitionStoryboardState();
        }

        public event PropertyChangedEventHandler PropertyChanged;        

        public string TitleText
        {
            get { return (string)GetValue(TitleTextProperty); }
            set { SetValue(TitleTextProperty, value); }
        }

        public string SummaryText
        {
            get { return (string)GetValue(SummaryTextProperty); }
            set { SetValue(SummaryTextProperty, value); }
        }

        public string PageTitle
        {
            get { return (string)GetValue(PageTitleProperty); }
            set { SetValue(PageTitleProperty, value); }
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

        public bool NetworkAvailable
        {
            get { return InternetConnection.IsInternetAvailable(); }
        }

        public bool NeedsFullScreen
        {
            get { return (bool)GetValue(NeedsFullScreenProperty); }
            set 
            { 
                SetValue(NeedsFullScreenProperty, value);
                OnPropertyChanged("NeedsFullScreen");
            }
        }

        private void OnOrientationChanged(DisplayInformation sender, object args)
        {
            this.TransitionStoryboardState();
        }

        private async void TransitionStoryboardState()
        {
            string displayOrientation;
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();            

            switch (DisplayInformation.GetForCurrentView().CurrentOrientation)
            {
                case DisplayOrientations.Landscape:
                case DisplayOrientations.LandscapeFlipped:
                    await statusBar.HideAsync();
                    NeedsFullScreen = true;
                    displayOrientation = "Landscape";
                    break;
                case DisplayOrientations.Portrait:
                case DisplayOrientations.PortraitFlipped:
                default:
                    await statusBar.ShowAsync();
                    NeedsFullScreen = false;
                    displayOrientation = "Portrait";
                    break;
            }

            VisualStateManager.GoToState(this, displayOrientation, false);
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
