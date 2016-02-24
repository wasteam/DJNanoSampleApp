


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp;
using Windows.ApplicationModel.Appointments;
using System.Linq;
using DJNanoShow.Config;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Sections
{
    public class DiscographyTracksConfig : SectionConfigBase<DiscographyTracks1Schema>
    {
		public override Func<Task<IEnumerable<DiscographyTracks1Schema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new LocalStorageDataConfig
                {
                    FilePath = "/Assets/Data/DiscographyTracks.json"
                };

                return () => Singleton<LocalStorageDataProvider<DiscographyTracks1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override bool NeedsNetwork
        {
            get
            {
                return false;
            }
        }

        public override ListPageConfig<DiscographyTracks1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<DiscographyTracks1Schema>
                {
                    Title = "Discography Tracks",

					PageTitle = "Discography Tracks",

                    ListNavigationInfo = NavigationInfo.FromPage("DiscographyTracksListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.Description.ToSafeString();
                        viewModel.Description = item.Description.ToSafeString();
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavigationInfo.FromPage("DiscographyTracksDetailPage", true);
                    }
                };
            }
        }

        public override DetailPageConfig<DiscographyTracks1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, DiscographyTracks1Schema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = item.Genre.ToSafeString();
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<DiscographyTracks1Schema>>
                {
                    ActionConfig<DiscographyTracks1Schema>.Play("Play", (item) => item.Link.ToSafeString()),
                };

                return new DetailPageConfig<DiscographyTracks1Schema>
                {
                    Title = "Discography Tracks",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
