using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DJNanoShow.Sections;
namespace DJNanoShow.ViewModels
{
    public class SearchViewModel : ObservableBase
    {
        public SearchViewModel() : base()
        {
			PageTitle = "DJNano Show";
            Tours = ListViewModel.CreateNew(Singleton<ToursConfig>.Instance);
            Videos = ListViewModel.CreateNew(Singleton<VideosConfig>.Instance);
            Gallery = ListViewModel.CreateNew(Singleton<GalleryConfig>.Instance);
            Instagram = ListViewModel.CreateNew(Singleton<InstagramConfig>.Instance);
            YouTube = ListViewModel.CreateNew(Singleton<YouTubeConfig>.Instance);
            Facebook = ListViewModel.CreateNew(Singleton<FacebookConfig>.Instance);
            Twitter = ListViewModel.CreateNew(Singleton<TwitterConfig>.Instance);
            Discography = ListViewModel.CreateNew(Singleton<DiscographyConfig>.Instance);
            DiscographyTracks = ListViewModel.CreateNew(Singleton<DiscographyTracksConfig>.Instance);
            Biography = ListViewModel.CreateNew(Singleton<BiographyConfig>.Instance);
                        
        }

        private string _searchText;
        private bool _hasItems = true;

        public string SearchText
        {
            get { return _searchText; }
            set { SetProperty(ref _searchText, value); }
        }

        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }

		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                async (text) =>
                {
                    await SearchDataAsync(text);
                }, SearchViewModel.CanSearch);
            }
        }      
        public ListViewModel Tours { get; private set; }
        public ListViewModel Videos { get; private set; }
        public ListViewModel Gallery { get; private set; }
        public ListViewModel Instagram { get; private set; }
        public ListViewModel YouTube { get; private set; }
        public ListViewModel Facebook { get; private set; }
        public ListViewModel Twitter { get; private set; }
        public ListViewModel Discography { get; private set; }
        public ListViewModel DiscographyTracks { get; private set; }
        public ListViewModel Biography { get; private set; }
        
		public string PageTitle { get; set; }
        public async Task SearchDataAsync(string text)
        {
            this.HasItems = true;
            SearchText = text;
            var loadDataTasks = GetViewModels()
                                    .Select(vm => vm.SearchDataAsync(text));

            await Task.WhenAll(loadDataTasks);
			this.HasItems = GetViewModels().Any(vm => vm.HasItems);
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Tours;
            yield return Videos;
            yield return Gallery;
            yield return Instagram;
            yield return YouTube;
            yield return Facebook;
            yield return Twitter;
            yield return Discography;
            yield return DiscographyTracks;
            yield return Biography;
        }
		private void CleanItems()
        {
            foreach (var vm in GetViewModels())
            {
                vm.CleanItems();
            }
        }
		public static bool CanSearch(string text) { return !string.IsNullOrWhiteSpace(text) && text.Length >= 3; }
    }
}
