using Windows.UI.Xaml.Navigation;
using AppStudio.Common;
using AppStudio.DataProviders.YouTube;
using DJNanoShow;
using DJNanoShow.Sections;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Views
{
    public sealed partial class VideosListPage : PageBase     {
        public VideosListPage()
        {
            this.ViewModel = new ListViewModel<YouTubeDataConfig, YouTubeSchema>(new VideosConfig());
            this.InitializeComponent();
}

        public ListViewModel<YouTubeDataConfig, YouTubeSchema> ViewModel { get; set; }
        protected async override void LoadState(object navParameter)
        {
            await this.ViewModel.LoadDataAsync();
        }

    }
}
