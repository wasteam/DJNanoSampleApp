using System;
using System.Windows.Input;
using AppStudio.Common;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using DJNanoShow.Navigation;

namespace DJNanoShow.ViewModels
{
    public class ShellViewModel : ObservableBase
    {
        private AppNavigation _navigation;
        private bool _navPanelOpened;
        private Visibility _hamburguerButtonVisibility;

        public ShellViewModel()
        {
            Navigation = new AppNavigation();
            Navigation.LoadNavigation();
            HamburguerButtonVisibility = Visibility.Visible;
            SetHamburguerButtonVisibility += ((sender, hamburguerButtonVisibility) => { HamburguerButtonVisibility = hamburguerButtonVisibility; });

            NavigationService.NavigatedToPage += NavigationService_NavigatedToPage;
            SystemNavigationManager.GetForCurrentView().BackRequested += ((sender, e) =>
            {
                if (NavigationService.CanGoBack())
                {
                    e.Handled = true;
                    NavigationService.GoBack();
                }
            });
        }

        public static event EventHandler<Visibility> SetHamburguerButtonVisibility;

        public Visibility HamburguerButtonVisibility
        {
            get { return _hamburguerButtonVisibility; }
            set { SetProperty(ref _hamburguerButtonVisibility, value); }
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
        public static void SetHamburguerButtonProperties(Visibility hamburguerButtonVisibility)
        {
            SetHamburguerButtonVisibility(null, hamburguerButtonVisibility);
        }


        private void NavigationService_NavigatedToPage(object sender, NavigatedEventArgs e)
        {
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
