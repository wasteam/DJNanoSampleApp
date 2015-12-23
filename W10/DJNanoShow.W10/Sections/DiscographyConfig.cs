


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using Windows.Storage;
using AppStudio.DataProviders.DynamicStorage;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using System.Linq;
using DJNanoShow.Config;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Sections
{
    public class DiscographyConfig : SectionConfigBase<Discography1Schema>
    {
	    public override Func<Task<IEnumerable<Discography1Schema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new DynamicStorageDataConfig
                {
                    Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=ee25a9dc-2850-4f61-848f-684fef6a345a&appId=543c63b8-2956-4450-8efd-1cf199bee7ed"),
                    AppId = "543c63b8-2956-4450-8efd-1cf199bee7ed",
                    StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                    DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string
                };

                return () => Singleton<DynamicStorageDataProvider<Discography1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override ListPageConfig<Discography1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Discography1Schema>
                {
                    Title = "Discography",

					PageTitle = "Discography",

                    ListNavigationInfo = NavigationInfo.FromPage("DiscographyListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title.ToSafeString();
                        viewModel.SubTitle = item.ReleaseDate.ToSafeString();
                        viewModel.Description = null;
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    },
                    DetailNavigation = (item) =>
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
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
                    viewModel.Content = null;
                });
                bindings.Add((viewModel, item) =>
                {
                    viewModel.PageTitle = "Tracks";
                    viewModel.Title = "";
                    viewModel.Description = item.Description.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl("");
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
    }
}
