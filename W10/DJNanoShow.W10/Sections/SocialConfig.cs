


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.DataProviders.Menu;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp;
using System.Linq;
using DJNanoShow.Config;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Sections
{
    public class SocialConfig : SectionConfigBase<MenuSchema>
    {
	    public override Func<Task<IEnumerable<MenuSchema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new LocalStorageDataConfig
                {
                    FilePath = "/Assets/Data/Social.json"
                };

                return () => Singleton<LocalStorageDataProvider<MenuSchema>>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override bool NeedsNetwork
        {
            get
            {
                return false;
            }
        }

        public override ListPageConfig<MenuSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<MenuSchema>
                {
                    Title = "Social",

					PageTitle = "Social",

                    ListNavigationInfo = NavigationInfo.FromPage("SocialListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Title = item.Title;						
                        viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.Icon);
                    },
                    DetailNavigation = (item) =>
                    {
                        return item.ToNavigationInfo();
                    }
                };
            }
        }

        public override DetailPageConfig<MenuSchema> DetailPage
        {
            get { return null; }
        }
    }
}
