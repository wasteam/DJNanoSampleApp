using AppStudio.Uwp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Services
{
    public class UserFavorites
    {
        private const string FavFileName = "UserFavorites";
        private static FavoritesTable _userFavorites;

        public IEnumerable<FavoritesSection> Sections
        {
            get
            {
                CheckInitialized();

                return _userFavorites.Sections;
            }
        }

        public ulong RoamingQuota
        {
            get
            {
                return ApplicationData.Current.RoamingStorageQuota;
            }
        }

        public bool IsOnFavorites(string sectionName, ItemViewModel item)
        {
            CheckInitialized();

            return _userFavorites.ContainsItem(sectionName, item.Id);
        }

        public async Task AddOrRemoveToFavoritesAsync(string sectionName, ItemViewModel item)
        {
            CheckInitialized();

            if (IsOnFavorites(sectionName, item))
            {
                _userFavorites.Remove(sectionName, item.Id);
            }
            else
            {
                _userFavorites.Add(sectionName, item.Id);
            }
            await Singleton<RoamingService>.Instance.SaveAsync(FavFileName, _userFavorites);
        }

        public static async Task InitAsync()
        {
            await LoadRoamingAsync();

            ApplicationData.Current.DataChanged += async (sender, args) => {
                await LoadRoamingAsync();
            };
        }

        private static async Task LoadRoamingAsync()
        {
            var fav = await Singleton<RoamingService>.Instance.GetAsync<FavoritesTable>(FavFileName);
            if (fav == null)
            {
                fav = new FavoritesTable
                {
                    Sections = new List<FavoritesSection>()
                };
            }
            _userFavorites = fav;
        }

        private void CheckInitialized()
        {
            if (_userFavorites == null)
            {
                throw new Exception("User favorites is not initialized. Call 'InitAsync' method before use this class.");
            }
        }
    }

    public class FavoritesTable
    {
        public List<FavoritesSection> Sections { get; set; }

        public bool ContainsItem(string section, string itemId)
        {
            if (Sections != null && Sections.Any(s => s.Name == section))
            {
                return Sections.First(s => s.Name == section).ItemsId.Any(i => i == itemId);
            }
            return false;
        }

        public void Add(string section, string itemId)
        {
            if (Sections == null)
            {
                Sections = new List<FavoritesSection>();
            }
            var targetSection = Sections.FirstOrDefault(s => s.Name == section);

            if (targetSection == null)
            {
                targetSection = new FavoritesSection
                {
                    Name = section,
                    ItemsId = new List<string>()
                };
                Sections.Add(targetSection);
            }
            targetSection.ItemsId.Add(itemId);
        }

        public void Remove(string section, string itemId)
        {
            if (Sections != null)
            {
                var targetSection = Sections.FirstOrDefault(s => s.Name == section);

                if (targetSection != null)
                {
                    targetSection.ItemsId.Remove(itemId);
                }
            }

        }
    }

    public class FavoritesSection
    {
        public string Name { get; set; }
        public List<string> ItemsId { get; set; }
    }
}
