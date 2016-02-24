using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows.Input;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp.Commands;
using AppStudio.DataProviders;

using AppStudio.DataProviders.YouTube;
using AppStudio.DataProviders.Instagram;
using AppStudio.DataProviders.Menu;
using AppStudio.DataProviders.Html;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.DynamicStorage;
using DJNanoShow.Sections;


namespace DJNanoShow.ViewModels
{
    public class MainViewModel : ObservableBase
    {
        public MainViewModel(int visibleItems) : base()
        {
            PageTitle = "DJNano Show";
            Tours = ListViewModel.CreateNew(Singleton<ToursConfig>.Instance, visibleItems);
            Videos = ListViewModel.CreateNew(Singleton<VideosConfig>.Instance, visibleItems);
            Gallery = ListViewModel.CreateNew(Singleton<GalleryConfig>.Instance, visibleItems);
            Social = ListViewModel.CreateNew(Singleton<SocialConfig>.Instance);
            Discography = ListViewModel.CreateNew(Singleton<DiscographyConfig>.Instance, visibleItems);
            Biography = ListViewModel.CreateNew(Singleton<BiographyConfig>.Instance, visibleItems);

            Actions = new List<ActionInfo>();

            if (GetViewModels().Any(vm => !vm.HasLocalData))
            {
                Actions.Add(new ActionInfo
                {
                    Command = new RelayCommand(Refresh),
                    Style = ActionKnownStyles.Refresh,
                    Name = "RefreshButton",
                    ActionType = ActionType.Primary
                });
            }
        }

        public string PageTitle { get; set; }
        public ListViewModel Tours { get; private set; }
        public ListViewModel Videos { get; private set; }
        public ListViewModel Gallery { get; private set; }
        public ListViewModel Social { get; private set; }
        public ListViewModel Discography { get; private set; }
        public ListViewModel Biography { get; private set; }

        public RelayCommand<INavigable> SectionHeaderClickCommand
        {
            get
            {
                return new RelayCommand<INavigable>(item =>
                    {
                        NavigationService.NavigateTo(item);
                    });
            }
        }
		public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand<string>(
                (text) =>
                {
                    NavigationService.NavigateToPage("SearchPage", text);
                }, SearchViewModel.CanSearch);
            }
        }

        public DateTime? LastUpdated
        {
            get
            {
                return GetViewModels().Select(vm => vm.LastUpdated)
                            .OrderByDescending(d => d).FirstOrDefault();
            }
        }

        public List<ActionInfo> Actions { get; private set; }

        public bool HasActions
        {
            get
            {
                return Actions != null && Actions.Count > 0;
            }
        }

        public async Task LoadDataAsync()
        {
            var loadDataTasks = GetViewModels().Select(vm => vm.LoadDataAsync());

            await Task.WhenAll(loadDataTasks);

            OnPropertyChanged("LastUpdated");
        }

        private async void Refresh()
        {
            var refreshDataTasks = GetViewModels()
                                        .Where(vm => !vm.HasLocalData).Select(vm => vm.LoadDataAsync(true));

            await Task.WhenAll(refreshDataTasks);

            OnPropertyChanged("LastUpdated");
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Tours;
            yield return Videos;
            yield return Gallery;
            yield return Social;
            yield return Discography;
            yield return Biography;
        }
    }
}
