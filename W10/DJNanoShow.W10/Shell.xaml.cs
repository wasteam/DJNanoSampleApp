using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using AppStudio.Common.Navigation;
using DJNanoShow.ViewModels;
using DJNanoShow.Views;

namespace DJNanoShow
{
    public sealed partial class Shell : Page
    {
        public Shell()
            : base()
        {
            this.ViewModel = new ShellViewModel();

            this.InitializeComponent();

            var applicationView = ApplicationView.GetForCurrentView();

            applicationView.TitleBar.BackgroundColor = (Color)Application.Current.Resources["AppBarBackgroundColor"];
            applicationView.TitleBar.ForegroundColor = (Color)Application.Current.Resources["AppBarForegroundColor"];
            applicationView.TitleBar.ButtonBackgroundColor = (Color)Application.Current.Resources["AppBarBackgroundColor"];
            applicationView.TitleBar.ButtonForegroundColor = (Color)Application.Current.Resources["AppBarForegroundColor"];
            applicationView.TitleBar.ButtonHoverBackgroundColor = (Color)Application.Current.Resources["AppBarForegroundColor"];
            applicationView.TitleBar.ButtonHoverForegroundColor = (Color)Application.Current.Resources["AppBarBackgroundColor"];
            applicationView.TitleBar.ButtonPressedBackgroundColor = (Color)Application.Current.Resources["AppBarForegroundColor"];
            applicationView.TitleBar.ButtonPressedForegroundColor = (Color)Application.Current.Resources["AppBarBackgroundColor"];

            this.Loaded += MainPage_Loaded;
        }
        
        public ShellViewModel ViewModel { get; set; }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService.Initialize(typeof(App), MainFrame);
            NavigationService.NavigateToPage(typeof(HomePage));
        }
    }
}
