using Windows.UI.Xaml.Navigation;
using AppStudio.Common;
using AppStudio.DataProviders.DynamicStorage;
using DJNanoShow;
using DJNanoShow.Sections;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Views
{
    public sealed partial class ToursListPage : PageBase     {
        public ToursListPage()
        {
            this.ViewModel = new ListViewModel<DynamicStorageDataConfig, Tours1Schema>(new ToursConfig());
            this.InitializeComponent();
}

        public ListViewModel<DynamicStorageDataConfig, Tours1Schema> ViewModel { get; set; }
        protected async override void LoadState(object navParameter)
        {
            await this.ViewModel.LoadDataAsync();
        }

    }
}
