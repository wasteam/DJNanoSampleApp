


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
    public class ToursConfig : SectionConfigBase<Tours1Schema>
    {
	    public override Func<Task<IEnumerable<Tours1Schema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new DynamicStorageDataConfig
                {
                    Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=c5f2660f-9fd0-4d53-9b14-157394920ffe&appId=543c63b8-2956-4450-8efd-1cf199bee7ed"),
                    AppId = "543c63b8-2956-4450-8efd-1cf199bee7ed",
                    StoreId = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.StoreId] as string,
                    DeviceType = ApplicationData.Current.LocalSettings.Values[LocalSettingNames.DeviceType] as string
                };

                return () => Singleton<DynamicStorageDataProvider<Tours1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override ListPageConfig<Tours1Schema> ListPage
        {
            get 
            {
                return new ListPageConfig<Tours1Schema>
                {
                    Title = "Tours",

					PageTitle = "Tours",

                    ListNavigationInfo = NavigationInfo.FromPage("ToursListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Where.ToSafeString();
                        viewModel.SubTitle = item.When.ToSafeString();
                        viewModel.Description = "";
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl("");
                    },
                    DetailNavigation = (item) =>
                    {
                        return null;
                    }
                };
            }
        }

        public override DetailPageConfig<Tours1Schema> DetailPage
        {
            get
            {
                var bindings = new List<Action<ItemViewModel, Tours1Schema>>();

                var actions = new List<ActionConfig<Tours1Schema>>
                {
                };

                return new DetailPageConfig<Tours1Schema>
                {
                    Title = "Tours",
                    LayoutBindings = bindings,
                    Actions = actions
                };
            }
        }
    }
}
