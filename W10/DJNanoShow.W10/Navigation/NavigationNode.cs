using System.Collections.ObjectModel;
using AppStudio.Common;
using AppStudio.Common.Navigation;
using Windows.UI.Xaml;

namespace DJNanoShow.Navigation
{
    public abstract class NavigationNode : ObservableBase
    {
        public string Title { get; set; }
        public string Label { get; set; }
        public string FontIcon { get; set; }
        public string Image { get; set; }
        public abstract bool IsContainer { get; }

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                SetProperty(ref _isSelected, value);
            }
        }
        public abstract void Selected();
    }

    public class ItemNavigationNode : NavigationNode, INavigable
    {
        public override bool IsContainer
        {
            get
            {
                return false;
            }
        }

        public NavigationInfo NavigationInfo { get; set; }

        public override void Selected()
        {
            NavigationService.NavigateTo(this);            
        }
    }

    public class GroupNavigationNode : NavigationNode
    {
        private Visibility _visibility;

        public GroupNavigationNode()
        {
            Nodes = new ObservableCollection<NavigationNode>();
        }

        public override bool IsContainer
        {
            get
            {
                return true;
            }
        }

        public ObservableCollection<NavigationNode> Nodes { get; set; }

        public Visibility Visibility
        {
            get { return _visibility; }
            set { SetProperty(ref _visibility, value); }
        }

        public override void Selected()
        {
            if (Visibility == Visibility.Collapsed)
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Collapsed;
            }
        }
    }
}
