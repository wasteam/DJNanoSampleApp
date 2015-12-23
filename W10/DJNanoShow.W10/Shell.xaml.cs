using System;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AppStudio.Uwp.Navigation;
using DJNanoShow.Services;
using DJNanoShow.ViewModels;
using DJNanoShow.Pages;

namespace DJNanoShow
{
    public sealed partial class Shell : Page
    {
        DispatcherTimer loadingTimer;
	    private bool isFullScreen = false;

        public Shell() : base()
        {
            this.ViewModel = new ShellViewModel();
			loadingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(600) };
            loadingTimer.Tick += ((sender, e) =>
            {
                loadingTimer.Stop();
                loadingProgressRing.IsActive = false;
                extendedLoadingHome.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            });
            this.InitializeComponent();

            var applicationView = ApplicationView.GetForCurrentView();

            this.Loaded += MainPage_Loaded;
            FullScreenService.FullScreenModeChanged += ((sender, e) => { isFullScreen = e; });
        }
        
        public ShellViewModel ViewModel { get; set; }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.Initialize(typeof(App), MainFrame);
            NavigationService.NavigateToPage(typeof(HomePage));
			loadingTimer.Start();
        }

		private void PageLayout_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (FullScreenService.CurrentPageSupportFullScreen)
            {
                if (e.Key == Windows.System.VirtualKey.F5)
                {
                    FullScreenService.EnterFullScreenMode(true);
                }
                else if (e.Key == Windows.System.VirtualKey.Escape)
                {
                    if (isFullScreen)
                    {
                        FullScreenService.ExitFullScreenMode();
                    }
                    else
                    {
                        TryGoBack();
                    }
                }
            }
			else
            {
                if (e.Key == Windows.System.VirtualKey.Escape)
                {
                    TryGoBack();
                }
            }
        }

        private void TryGoBack()
        {
            if (NavigationService.CanGoBack())
            {
                NavigationService.GoBack();
            }
        }
    }
}
