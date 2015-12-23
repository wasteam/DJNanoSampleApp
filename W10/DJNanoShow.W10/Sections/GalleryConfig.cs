


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Instagram;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp;
using System.Linq;
using DJNanoShow.Config;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Sections
{
    public class GalleryConfig : SectionConfigBase<InstagramSchema>
    {
		private readonly InstagramDataProvider _dataProvider = new InstagramDataProvider(new InstagramOAuthTokens
        {
			ClientId = "1256362be0e0404e9e1228f39c899ca0"
        });

		public override Func<Task<IEnumerable<InstagramSchema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new InstagramDataConfig
                {
                    QueryType = InstagramQueryType.Id,
                    Query = @"11102629"
                };

                return () => _dataProvider.LoadDataAsync(config, MaxRecords);
            }
        }

        public override ListPageConfig<InstagramSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<InstagramSchema>
                {
                    Title = "Gallery",

					PageTitle = "Gallery",

                    ListNavigationInfo = NavigationInfo.FromPage("GalleryListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = "";
                        viewModel.SubTitle = "";
                        viewModel.Description = null;
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ThumbnailUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavigationInfo.FromPage("GalleryDetailPage", true);
                    }
                };
            }
        }

        public override DetailPageConfig<InstagramSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, InstagramSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = item.Title.ToSafeString();
                    viewModel.Description = item.Author.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<InstagramSchema>>
                {
                    ActionConfig<InstagramSchema>.Link("Go To Source", (item) => item.SourceUrl.ToSafeString()),
                };

                return new DetailPageConfig<InstagramSchema>
                {
                    Title = "Gallery",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
