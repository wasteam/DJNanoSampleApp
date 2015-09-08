using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AppStudio.Common.Actions;
using AppStudio.Common.Cache;
using AppStudio.Common.Commands;
using AppStudio.DataProviders;
using Windows.ApplicationModel.DataTransfer;
using DJNanoShow.Config;
using Windows.UI.Xaml;
using DJNanoShow.Services;

namespace DJNanoShow.ViewModels
{
    public class DetailViewModel<TConfig, TSchema> : DataViewModelBase<TConfig, TSchema> where TSchema : SchemaBase
    {
        private SectionConfigBase<TConfig, TSchema> _sectionConfig;
        private ComposedItemViewModel _selectedItem;
        private bool _isFullScreen;

        public DetailViewModel(SectionConfigBase<TConfig, TSchema> sectionConfig)
            : base(sectionConfig)
        {
            Items = new ObservableCollection<ComposedItemViewModel>();
            FullScreenService.FullScreenModeChanged += FullScreenModeChanged;
            _sectionConfig = sectionConfig;

            Title = _sectionConfig.DetailPage.Title;
        }

        private void FullScreenModeChanged(object sender, bool isFullScreen)
        {
            this.IsFullScreen = isFullScreen;
        }

        public ComposedItemViewModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }

        public ObservableCollection<ComposedItemViewModel> Items { get; protected set; }

        public bool IsFullScreen
        {
            get { return _isFullScreen; }
            set { SetProperty(ref _isFullScreen, value); }
        }
        public Visibility FullScreenCommandVisibility
        {
            get
            {
                var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
                var isOnMobile = qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"].ToLowerInvariant() == "Mobile".ToLowerInvariant();
                if (isOnMobile)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }

        public ICommand FullScreenCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    FullScreenService.ChangeFullScreenMode();
                });
            }
        }

        public void ShareContent(DataRequest dataRequest, bool supportsHtml = true)
        {
            ShareContent(dataRequest, SelectedItem, supportsHtml);
        }

        protected override void ParseItems(CachedContent<TSchema> content, ItemViewModel selectedItem)
        {

            foreach (var item in content.Items)
            {
                var composedItem = new ComposedItemViewModel
                {
                    Id = item._id
                };

                foreach (var binding in _sectionConfig.DetailPage.LayoutBindings)
                {
                    var parsedItem = new ItemViewModel
                    {
                        Id = item._id
                    };
                    binding(parsedItem, item);

                    composedItem.Add(parsedItem);
                }

                composedItem.Actions = _sectionConfig.DetailPage.Actions
                                                                    .Select(a => new ActionInfo
                                                                    {
                                                                        Command = a.Command,
                                                                        CommandParameter = a.CommandParameter(item),
                                                                        Style = a.Style,
                                                                        Text = a.Text,
                                                                        ActionType = ActionType.Primary
                                                                    })
                                                                    .ToList();

                Items.Add(composedItem);
            }
            if (selectedItem != null)
            {
                SelectedItem = Items.FirstOrDefault(i => i.Id == selectedItem.Id);
            }

        }
    }
}
