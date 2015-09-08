using System;
using System.Windows.Input;
using AppStudio.Common;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using DJNanoShow.Navigation;
using Windows.UI.Xaml.Controls;
using DJNanoShow.Services;

namespace DJNanoShow.ViewModels
{
    public class ShellViewModel : ObservableBase
    {
        private AppNavigation _navigation;
        private bool _navPanelOpened;
        private bool _checkSizeChanged = true;
        public string CurrentPageName;
        private SplitViewDisplayMode _splitViewDisplayMode;
        private Visibility _hamburguerButtonVisibility;
        public Visibility HamburguerButtonVisibility
        {
            get { return _hamburguerButtonVisibility; }
            set { SetProperty(ref _hamburguerButtonVisibility, value); }
        }
        public static void SetHamburguerButtonProperties(Visibility hamburguerButtonVisibility)
        {
            SetHamburguerButtonVisibility(null, hamburguerButtonVisibility);
        }
        public static event EventHandler<Visibility> SetHamburguerButtonVisibility;

        public ShellViewModel()
        {
            Navigation = new AppNavigation();
            Navigation.LoadNavigation();
            if (Window.Current.Bounds.Width < 800)
            {
                this.SplitViewDisplayMode = SplitViewDisplayMode.Overlay;
            }
            else
            {
                this.SplitViewDisplayMode = SplitViewDisplayMode.CompactOverlay;
            }
            HamburguerButtonVisibility = Visibility.Visible;
            SetHamburguerButtonVisibility += ((sender, hamburguerButtonVisibility) => { HamburguerButtonVisibility = hamburguerButtonVisibility; });
            Window.Current.SizeChanged += WindowSizeChanged;
            NavigationService.NavigatedToPage += NavigationService_NavigatedToPage;
            FullScreenService.FullScreenModeChanged += FullScreenModeChanged;
            SystemNavigationManager.GetForCurrentView().BackRequested += ((sender, e) =>
            {
                if (NavigationService.CanGoBack())
                {
                    e.Handled = true;
                    NavigationService.GoBack();
                }
            });
        }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            if (_checkSizeChanged == false) return;
            if (e.Size.Width <= 800)
            {
                this.SplitViewDisplayMode = SplitViewDisplayMode.Overlay;
            }
            else
            {
                this.SplitViewDisplayMode = SplitViewDisplayMode.CompactOverlay;
            }
        }

        private void FullScreenModeChanged(object sender, bool isFullScreenModeEnabled)
        {
            if (isFullScreenModeEnabled)
            {
                HamburguerButtonVisibility = Visibility.Collapsed;
                _checkSizeChanged = false;
                this.SplitViewDisplayMode = SplitViewDisplayMode.Overlay;
            }
            else
            {
                HamburguerButtonVisibility = Visibility.Visible;
                _checkSizeChanged = true;
            }
        }

        public AppNavigation Navigation
        {
            get { return _navigation; }
            set { SetProperty(ref _navigation, value); }
        }

        public bool NavPanelOpened
        {
            get { return _navPanelOpened; }
            set { SetProperty(ref _navPanelOpened, value); }
        }

        public SplitViewDisplayMode SplitViewDisplayMode
        {
            get { return _splitViewDisplayMode; }
            set { SetProperty(ref _splitViewDisplayMode, value); }
        }

        public ICommand ItemSelected
        {
            get
            {
                return new RelayCommand<NavigationNode>(n =>
                {
                    n.Selected();
                });
            }
        }

        public ICommand NavPanelClick
        {
            get
            {
                return new RelayCommand(() =>
                {
                    NavPanelOpened = !NavPanelOpened;
                });
            }
        }

        public ICommand GoBackCommand
        {
            get
            {
                return NavigationService.GoBackCommand;
            }
        }

        private void NavigationService_NavigatedToPage(object sender, NavigatedEventArgs e)
        {
            CurrentPageName = e.Page.FullName;
            var navigatedNode = Navigation.FindPage(e.Page);
            if (navigatedNode != null)
            {
                Navigation.Active = navigatedNode;
            }
            else
            {
                Navigation.Active = null;
            }

            if (NavPanelOpened)
            {
                NavPanelOpened = false;
            }
            if (NavigationService.CanGoBack())
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
            OnPropertyChanged("GoBackCommand");
        }
    }
}
