using Windows.UI.Xaml.Navigation;
using AppStudio.Common;
using AppStudio.DataProviders.DynamicStorage;
using DJNanoShow;
using DJNanoShow.Sections;
using DJNanoShow.ViewModels;
using System.Windows.Input;
using AppStudio.Common.Actions;
using System.Collections.Generic;
using AppStudio.Common.Commands;

namespace DJNanoShow.Views
{
    public sealed partial class ToursListPage : PageBase
    {
        public ToursListPage()
        {
            this.ViewModel = new ListViewModel<DynamicStorageDataConfig, Tours1Schema>(new ToursConfig());
            this.InitializeComponent();
            Actions = new List<ActionInfo>();
            Actions.Add(new ActionInfo
            {
                Command = Refresh,
                Style = ActionKnownStyles.Refresh,
                Name = "RefreshButton",
                ActionType = ActionType.Primary
            });
        }

        public ListViewModel<DynamicStorageDataConfig, Tours1Schema> ViewModel { get; set; }
        protected async override void LoadState(object navParameter)
        {
            await this.ViewModel.LoadDataAsync();
            ToursConfig.RemoveDeprecatedTours(ViewModel.Items);
        }
        public List<ActionInfo> Actions { get; private set; }
        public ICommand Refresh
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    await this.ViewModel.LoadDataAsync(true);
                    ToursConfig.RemoveDeprecatedTours(ViewModel.Items);
                });
            }
        }
    }
}
