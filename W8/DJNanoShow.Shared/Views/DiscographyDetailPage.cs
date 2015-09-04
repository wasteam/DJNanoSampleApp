using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Navigation;
using AppStudio.DataProviders.DynamicStorage;
using DJNanoShow;
using DJNanoShow.Sections;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Views

{
    public sealed partial class DiscographyDetailPage : PageBase
    {
        private DataTransferManager _dataTransferManager;
        public DetailViewModel<DynamicStorageDataConfig, Discography1Schema> ViewModel { get; set; }

        public DiscographyDetailPage()
        {
            this.ViewModel = new DetailViewModel<DynamicStorageDataConfig, Discography1Schema>(new DiscographyConfig());
            this.InitializeComponent();            
        }

        protected async override void LoadState(object navParameter)
        {
            await this.ViewModel.LoadDataAsync(navParameter as ItemViewModel);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _dataTransferManager = DataTransferManager.GetForCurrentView();
            _dataTransferManager.DataRequested += OnDataRequested;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _dataTransferManager.DataRequested -= OnDataRequested;

            base.OnNavigatedFrom(e);
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            bool supportsHtml = true;
#if WINDOWS_PHONE_APP
            supportsHtml = false;
#endif
            ViewModel.ShareContent(args.Request, supportsHtml);
        }
    }
}
