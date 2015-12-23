


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Twitter;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp;
using System.Linq;
using DJNanoShow.Config;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Sections
{
    public class TwitterConfig : SectionConfigBase<TwitterSchema>
    {
		private readonly TwitterDataProvider _dataProvider = new TwitterDataProvider(new TwitterOAuthTokens
        {
			ConsumerKey = "4TQl0I6oUl1uD41E7H5WCw0X3",
                    ConsumerSecret = "ZouYN6qT6S5WGh2aVWAR2n74trdvpDGsHjUFjygod8FGX6UxQo",
                    AccessToken = "2412307028-KDGmWBNbDO8yZqv5h896OOnVNxoQUShNKMagg9X",
                    AccessTokenSecret = "wwhhCOu2GSWgwp0w9unRxoBJ9CxAUzxPxmiuZ6o9pdI3H"
        });

		public override Func<Task<IEnumerable<TwitterSchema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new TwitterDataConfig
                {
                    QueryType = TwitterQueryType.User,
                    Query = @"djnanoshow"
                };

                return () => _dataProvider.LoadDataAsync(config, MaxRecords);
            }
        }

        public override ListPageConfig<TwitterSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<TwitterSchema>
                {
                    Title = "Twitter",

					PageTitle = "Twitter",

                    ListNavigationInfo = NavigationInfo.FromPage("TwitterListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.UserName.ToSafeString();
                        viewModel.SubTitle = item.Text.ToSafeString();
                        viewModel.Description = null;
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.UserProfileImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
                    {
                        return new NavigationInfo
                        {
                            NavigationType = NavigationType.DeepLink,
                            TargetUri = new Uri(item.Url)
                        };
                    }
                };
            }
        }

        public override DetailPageConfig<TwitterSchema> DetailPage
        {
            get { return null; }
        }
    }
}
