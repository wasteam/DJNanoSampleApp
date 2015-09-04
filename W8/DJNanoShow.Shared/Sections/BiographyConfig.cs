using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using AppStudio.DataProviders.Html;
using AppStudio.DataProviders.LocalStorage;
using DJNanoShow.Config;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Sections
{
    public class BiographyConfig : SectionConfigBase<LocalStorageDataConfig, HtmlSchema>
    {
        public override DataProviderBase<LocalStorageDataConfig, HtmlSchema> DataProvider
        {
            get
            {
                return new HtmlDataProvider();
            }
        }

        public override LocalStorageDataConfig Config
        {
            get
            {
                return new LocalStorageDataConfig
                {
                    FilePath = "/Assets/Data/Biography.htm"
                };
            }
        }


        public override NavigationInfo ListNavigationInfo
        {
            get 
            {
                return NavigationInfo.FromPage("BiographyListPage");
            }
        }


        public override ListPageConfig<HtmlSchema> ListPage
        {
            get 
            {
                return new ListPageConfig<HtmlSchema>
                {
                    Title = "Biography",

                    LayoutBindings = (viewModel, item) =>
                    {
                        viewModel.Content = item.Content;
                    },
                    NavigationInfo = (item) =>
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

        public override string PageTitle
        {
            get { return "Biography"; }
        }

    }
}
