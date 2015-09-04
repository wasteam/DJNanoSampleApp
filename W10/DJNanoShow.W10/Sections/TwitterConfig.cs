using System;
using System.Collections.Generic;
using AppStudio.Common.Actions;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Twitter;
using DJNanoShow.Config;
using DJNanoShow.ViewModels;
using Microsoft.AppStudio.Core.Extensions;

namespace DJNanoShow.Sections
{
    public class TwitterConfig : SectionConfigBase<TwitterDataConfig, TwitterSchema>
    {
        public override DataProviderBase<TwitterDataConfig, TwitterSchema> DataProvider
        {
            get
            {
                return new TwitterDataProvider(new TwitterOAuthTokens
                {
                    ConsumerKey = "4TQl0I6oUl1uD41E7H5WCw0X3",
                        ConsumerSecret = "ZouYN6qT6S5WGh2aVWAR2n74trdvpDGsHjUFjygod8FGX6UxQo",
                        AccessToken = "2412307028-KDGmWBNbDO8yZqv5h896OOnVNxoQUShNKMagg9X",
                        AccessTokenSecret = "wwhhCOu2GSWgwp0w9unRxoBJ9CxAUzxPxmiuZ6o9pdI3H"

                });
            }
        }

        public override TwitterDataConfig Config
        {
            get
            {
                return new TwitterDataConfig
                {
                    QueryType = TwitterQueryType.User,
                    Query = @"djnanoshow"
                };
            }
        }

        public override NavigationInfo ListNavigationInfo
        {
            get 
            {
                return NavigationInfo.FromPage("TwitterListPage");
            }
        }


        public override ListPageConfig<TwitterSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<TwitterSchema>
                {
                    Title = "Twitter",

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.UserName.ToSafeString();
                        viewModel.SubTitle = item.Text.ToSafeString();
                        viewModel.Description = item.CreationDateTime.FriendlyDate();
                        viewModel.Image = item.UserProfileImageUrl.ToSafeString();

                    },
                    NavigationInfo = (item) =>
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

        public override string PageTitle
        {
            get { return "Twitter"; }
        }

    }
}
