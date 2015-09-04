using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppStudio.Common;
using AppStudio.Common.Actions;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.YouTube;
using AppStudio.DataProviders.Menu;
using AppStudio.DataProviders.Html;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.DynamicStorage;
using DJNanoShow.Sections;


namespace DJNanoShow.ViewModels
{
    public class MainViewModel : ObservableBase
    {
        public MainViewModel(int visibleItems)
        {
            PageTitle = "DJNano Show";
            Tours = new ListViewModel<DynamicStorageDataConfig, Tours1Schema>(new ToursConfig());
            Videos = new ListViewModel<YouTubeDataConfig, YouTubeSchema>(new VideosConfig(), visibleItems);
            Social = new ListViewModel<LocalStorageDataConfig, MenuSchema>(new SocialConfig());
            Discography = new ListViewModel<DynamicStorageDataConfig, Discography1Schema>(new DiscographyConfig(), visibleItems);
            Biography = new ListViewModel<LocalStorageDataConfig, HtmlSchema>(new BiographyConfig(), visibleItems);
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
        public ListViewModel<DynamicStorageDataConfig, Tours1Schema> Tours { get; private set; }
        public ListViewModel<YouTubeDataConfig, YouTubeSchema> Videos { get; private set; }
        public ListViewModel<LocalStorageDataConfig, MenuSchema> Social { get; private set; }
        public ListViewModel<DynamicStorageDataConfig, Discography1Schema> Discography { get; private set; }
        public ListViewModel<LocalStorageDataConfig, HtmlSchema> Biography { get; private set; }

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
            ToursConfig.RemoveDeprecatedTours(Tours.Items);
        }

        private async void Refresh()
        {
            var refreshDataTasks = GetViewModels()
                                        .Where(vm => !vm.HasLocalData)
                                        .Select(vm => vm.LoadDataAsync(true));

            await Task.WhenAll(refreshDataTasks);

            OnPropertyChanged("LastUpdated");
            ToursConfig.RemoveDeprecatedTours(Tours.Items);
        }

        private IEnumerable<DataViewModelBase> GetViewModels()
        {
            yield return Tours;
            yield return Videos;
            yield return Social;
            yield return Discography;
            yield return Biography;
        }
    }
}
