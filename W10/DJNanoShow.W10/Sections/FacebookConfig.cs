


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Facebook;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using DJNanoShow.Config;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Sections
{
    public class FacebookConfig : SectionConfigBase<FacebookSchema>
    {
		private readonly FacebookDataProvider _dataProvider = new FacebookDataProvider(new FacebookOAuthTokens
        {
			AppId = "599862193478955",
                    AppSecret = "83c6e8bf7be41f3bdeeb246ec16e106b"
        });

		public override Func<Task<IEnumerable<FacebookSchema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new FacebookDataConfig
                {
                    UserId = "372977625450"
                };

                return () => _dataProvider.LoadDataAsync(config, MaxRecords);
            }
        }

        public override ListPageConfig<FacebookSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<FacebookSchema>
                {
                    Title = "Facebook",

					PageTitle = "Facebook",

                    ListNavigationInfo = NavigationInfo.FromPage("FacebookListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Author.ToSafeString();
                        viewModel.SubTitle = item.Summary.ToSafeString();
                        viewModel.Description = "";
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return NavigationInfo.FromPage("FacebookDetailPage", true);
                    }
                };
            }
        }

        public override DetailPageConfig<FacebookSchema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, FacebookSchema>>();
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Author.ToSafeString();
                    viewModel.Title = "";
                    viewModel.Description = item.Content.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });

                var actions = new List<ActionConfig<FacebookSchema>>
                {
                    ActionConfig<FacebookSchema>.Link("Go To Source", (item) => item.FeedUrl.ToSafeString()),
                };

                return new DetailPageConfig<FacebookSchema>
                {
                    Title = "Facebook",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
