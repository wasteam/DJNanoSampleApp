using Windows.UI.Xaml.Navigation;
using AppStudio.Common;
using AppStudio.DataProviders.DynamicStorage;
using DJNanoShow;
using DJNanoShow.Sections;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Views
{
    public sealed partial class DiscographyListPage : PageBase     {
        public DiscographyListPage()
        {
            this.ViewModel = new ListViewModel<DynamicStorageDataConfig, Discography1Schema>(new DiscographyConfig());
            this.InitializeComponent();
}

        public ListViewModel<DynamicStorageDataConfig, Discography1Schema> ViewModel { get; set; }
        protected async override void LoadState(object navParameter)
        {
            await this.ViewModel.LoadDataAsync();
        }

    }
}
