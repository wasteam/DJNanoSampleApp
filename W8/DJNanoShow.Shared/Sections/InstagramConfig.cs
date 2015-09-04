using System;
using System.Collections.Generic;
using AppStudio.Common.Actions;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Instagram;
using DJNanoShow.Config;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Sections
{
    public class InstagramConfig : SectionConfigBase<InstagramDataConfig, InstagramSchema>
    {
        public override DataProviderBase<InstagramDataConfig, InstagramSchema> DataProvider
        {
            get
            {
                return new InstagramDataProvider(new InstagramOAuthTokens
                {
                    ClientId = "1256362be0e0404e9e1228f39c899ca0"

                });
            }
        }

        public override InstagramDataConfig Config
        {
            get
            {
                return new InstagramDataConfig
                {
                    QueryType = InstagramQueryType.Id,
                    Query = @"11102629"
                };
            }
        }

        public override NavigationInfo ListNavigationInfo
        {
            get 
            {
                return NavigationInfo.FromPage("InstagramListPage");
            }
        }

        public override ListPageConfig<InstagramSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<InstagramSchema>
                {
                    Title = "Instagram",

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = "";
                        viewModel.Description = null;
                        viewModel.Image = item.ThumbnailUrl.ToSafeString();

                    },
                    NavigationInfo = (item) =>
                    {
                        return NavigationInfo.FromPage("InstagramDetailPage", true);
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
                    viewModel.Image = item.ImageUrl.ToSafeString();
                    viewModel.Content = null;
                });

				var actions = new List<ActionConfig<InstagramSchema>>
				{
                    ActionConfig<InstagramSchema>.Link("Go To Source", (item) => item.SourceUrl.ToSafeString()),
				};

                return new DetailPageConfig<InstagramSchema>
                {
                    Title = "Instagram",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }

        public override string PageTitle
        {
            get { return "Instagram"; }
        }

    }
}
