using System;
using System.Collections.Generic;
using AppStudio.Uwp;
using AppStudio.Uwp.Commands;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Input;
using DJNanoShow.Services;
using DJNanoShow.Sections;
namespace DJNanoShow.ViewModels
{
    public class FavoritesViewModel : ObservableBase
    {
        public FavoritesViewModel() : base()
        {
			PageTitle = "Favorites";

            Tours = ListViewModel.CreateNew(Singleton<ToursConfig>.Instance);
            Discography = ListViewModel.CreateNew(Singleton<DiscographyConfig>.Instance);

			ShowRoamingWarning = Singleton<UserFavorites>.Instance.RoamingQuota == 0;                       
        }     
        public ListViewModel Tours { get; private set; }
        public ListViewModel Discography { get; private set; }

        private bool _showRoamingWarning;
        public bool ShowRoamingWarning
        {
            get { return _showRoamingWarning; }
            set { SetProperty(ref _showRoamingWarning, value); }
        }
		private bool _hasItems = true;
        public bool HasItems
        {
            get { return _hasItems; }
            set { SetProperty(ref _hasItems, value); }
        }       
		public string PageTitle { get; set; }

        public async Task LoadDataAsync()
        {
            this.HasItems = true;
            List<Task> loadDataTasks = new List<Task>();

            if (Singleton<UserFavorites>.Instance.Sections != null)
            {
                foreach (var favInSection in Singleton<UserFavorites>.Instance.Sections)
                {
                    var vm = GetSectionViewModel(favInSection.Name);

                    if (vm != null)
                    {
                        loadDataTasks.Add(vm.FilterDataAsync(favInSection.ItemsId));
                    }
                } 
            }

            await Task.WhenAll(loadDataTasks);
            this.HasItems = GetViewModels().Any(vm => vm.HasItems);
        }

        private IEnumerable<ListViewModel> GetViewModels()
        {
            yield return Tours;
            yield return Discography;
        }

		private ListViewModel GetSectionViewModel(string sectionName)
        {
            return GetViewModels().FirstOrDefault(vm => vm.SectionName == sectionName);
        }
    }
}
