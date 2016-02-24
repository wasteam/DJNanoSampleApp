


using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AppStudio.DataProviders;
using AppStudio.DataProviders.Core;
using Windows.Storage;
using AppStudio.DataProviders.DynamicStorage;
using AppStudio.DataProviders.LocalStorage;
using AppStudio.Uwp;
using AppStudio.Uwp.Actions;
using AppStudio.Uwp.Commands;
using AppStudio.Uwp.Navigation;
using Windows.ApplicationModel.Appointments;
using System.Linq;
using DJNanoShow.Config;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Sections
{
    public class DiscographyConfig : SectionConfigBase<Discography1Schema, DiscographyTracks1Schema>
    {
	    public override Func<Task<IEnumerable<Discography1Schema>>> LoadDataAsyncFunc
        {
            get
            {
                var config = new DynamicStorageDataConfig
                {
                    Url = new Uri("http://ds.winappstudio.com/api/data/collection?dataRowListId=8a3580ec-aa0e-419e-8092-8c7ad63a54bb&appId=543c63b8-2956-4450-8efd-1cf199bee7ed"),
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
                        viewModel.SubTitle = item.LabelName.ToSafeString();
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
                    viewModel.Title = item.ReleaseDate.ToString(DateTimeFormat.LongDate);
                    viewModel.Description = item.LabelName.ToSafeString();
                    viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(item.ImageUrl.ToSafeString());
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
		public override RelatedContentConfig<DiscographyTracks1Schema, Discography1Schema> RelatedContent
		{
			get
			{
				return new RelatedContentConfig<DiscographyTracks1Schema, Discography1Schema>()
				{
					NeedsNetwork = false,
					LoadDataAsync = async (selected) =>
					{
						var config = new LocalStorageDataConfig
						{
							FilePath = "/Assets/Data/DiscographyTracks.json"
						};
						var result = await Singleton<LocalStorageDataProvider<DiscographyTracks1Schema>>.Instance.LoadDataAsync(config, MaxRecords);
						return result
								.Where(r => r.Title.ToSafeString() == selected.Title.ToSafeString())
								.ToList();
					},
					ListPage = new ListPageConfig<DiscographyTracks1Schema>
					{
						Title = "Tracks",

						LayoutBindings = (viewModel, item) =>
						{
							viewModel.Title = item.Genre.ToSafeString();
							viewModel.SubTitle = item.Tracks.ToSafeString();
							viewModel.Description = null;
							viewModel.ImageUrl = ItemViewModel.LoadSafeUrl(null);
						},
						DetailNavigation = (item) =>
						{
							return NavigationInfo.FromPage("DiscographyTracksDetailPage", true);
						}
					}
				};
			}
		}
    }
}
