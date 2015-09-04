using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using AppStudio.Common.Actions;
using AppStudio.Common.Cache;
using AppStudio.Common.Commands;
using AppStudio.DataProviders;
using Windows.ApplicationModel.DataTransfer;
using DJNanoShow.Config;

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

            _sectionConfig = sectionConfig;

            Title = _sectionConfig.DetailPage.Title;
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

        public ICommand FullScreenCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    this.IsFullScreen = !_isFullScreen;
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
