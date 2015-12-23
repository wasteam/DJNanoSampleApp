


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Html;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.Uwp.Navigation;
using AppStudio.Uwp;
using System.Linq;
using DJNanoShow.Config;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Sections
{
    public class BiographyConfig : SectionConfigBase<HtmlSchema>
    {
		public override Func<Task<IEnumerable<HtmlSchema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new LocalStorageDataConfig
                {
                    FilePath = "/Assets/Data/Biography.htm"
                };

                return () => Singleton<HtmlDataProvider>.Instance.LoadDataAsync(config, MaxRecords);
            }
        }

        public override bool NeedsNetwork
        {
            get
            {
                return false;
            }
        }

        public override ListPageConfig<HtmlSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<HtmlSchema>
                {
                    Title = "Biography",

					PageTitle = "Biography",

                    ListNavigationInfo = NavigationInfo.FromPage("BiographyListPage"),

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Content = item.Content;
                    },
                    DetailNavigation = (item) =>
                    {
                        return null;
                    }
                };
            }
        }

        public override DetailPageConfig<HtmlSchema> DetailPage
        {
            get { return null; }
        }
    }
}
