using System;
using System.Collections.Generic;
using AppStudio.Common;
using AppStudio.Common.Actions;
using AppStudio.Common.Commands;
using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.DynamicStorage;
using Windows.Storage;
using DJNanoShow.Config;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Sections
{
    public class DiscographyConfig : SectionConfigBase<DynamicStorageDataConfig, Discography1Schema>
    {
        public override DataProviderBase<DynamicStorageDataConfig, Discography1Schema> DataProvider
        {
            get
            {
                return new DynamicStorageDataProvider<Discography1Schema>();
            }
        }

        public override DynamicStorageDataConfig Config
        {
            get
            {
                return new DynamicStorageDataConfig
                {
                    Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=3a102651-2d2f-4a86-b616-cbddb10e0e7e&appId=2a4aecfd-7ef0-487a-b45f-785033404119"),
                    AppId = "2a4aecfd-7ef0-487a-b45f-785033404119",
                    StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                    DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string
                };
            }
        }

        public override NavigationInfo ListNavigationInfo
        {
            get 
            {
                return NavigationInfo.FromPage("DiscographyListPage");
            }
        }

        public override ListPageConfig<Discography1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Discography1Schema>
                {
                    Title = "Discography",

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.ReleaseDate.ToSafeString();
                        viewModel.Description = null;
                        viewModel.Image = item.ImageUrl.ToSafeString();

                    },
                    NavigationInfo = (item) =>
                    {
                        return NavigationInfo.FromPage("DiscographyDetailPage", true);
                    }
                };
            }
        }

        public override DetailPageConfig<Discography1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Discography1Schema>>();

                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = item.Title.ToSafeString();
                    viewModel.Title = "";
                    viewModel.Description = item.ReleaseDate.ToSafeString();
                    viewModel.Image = item.ImageUrl.ToSafeString();
                    viewModel.Content = null;
                });

                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Tracks";
                    viewModel.Title = "";
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.Image = "";
                    viewModel.Content = null;
                });

				var actions = new List<ActionConfig<Discography1Schema>>
				{
                    ActionConfig<Discography1Schema>.Play("Play", (item) => item.Link.ToSafeString()),
				};

                return new DetailPageConfig<Discography1Schema>
                {
                    Title = "Discography",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }

        public override string PageTitle
        {
            get { return "Discography"; }
        }

    }
}
