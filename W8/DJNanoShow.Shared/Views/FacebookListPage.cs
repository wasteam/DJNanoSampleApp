using Windows.UI.Xaml.Navigation;
using AppStudio.Common;
using AppStudio.DataProviders.Facebook;
using DJNanoShow;
using DJNanoShow.Sections;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Views
{
    public sealed partial class FacebookListPage : PageBase
    {
        public ListViewModel<FacebookDataConfig, FacebookSchema> ViewModel { get; set; }

        public FacebookListPage()
        {
            this.ViewModel = new ListViewModel<FacebookDataConfig, FacebookSchema>(new FacebookConfig());
            this.InitializeComponent();
        }

        protected async override void LoadState(object navParameter)
        {
            await this.ViewModel.LoadDataAsync();
        }

    }
}
