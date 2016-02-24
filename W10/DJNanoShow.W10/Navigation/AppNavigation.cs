using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppStudio.Uwp.Navigation;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;

namespace DJNanoShow.Navigation
{
    public class AppNavigation
    {
        private NavigationNode _active;

        static AppNavigation()
        {

        }

        public NavigationNode Active
        {
            get
            {
                return _active;
            }
            set
            {
                if (_active != null)
                {
                    _active.IsSelected = false;
                }
                _active = value;
                if (_active != null)
                {
                    _active.IsSelected = true;
                }
            }
        }


        public ObservableCollection<NavigationNode> Nodes { get; private set; }

        public void LoadNavigation()
        {
            Nodes = new ObservableCollection<NavigationNode>();
		    var resourceLoader = new ResourceLoader();
			AddNode(Nodes, "Home", "\ue10f", string.Empty, "HomePage", true, @"DJNano Show");
				AddNode(Nodes, "Favorites", "\ue113", string.Empty, "FavoritesPage", true);
				AddNode(Nodes, "Tours", "\ue1d3", string.Empty, "ToursListPage", true);			
			AddNode(Nodes, "Videos", "\ue173", string.Empty, "VideosListPage", true);			
			AddNode(Nodes, "Gallery", "\ue12d", string.Empty, "GalleryListPage", false);			
			var menuNodeSocial = new GroupNavigationNode("Social", "\ue10c", string.Empty);
			AddNode(menuNodeSocial.Nodes, "Twitter", string.Empty, "/Assets/DataImages/1456185609_03-twitte.png", "TwitterListPage");
			AddNode(menuNodeSocial.Nodes, "Facebook", string.Empty, "/Assets/DataImages/1456185579_06-facebo.png", "FacebookListPage");
			AddNode(menuNodeSocial.Nodes, "YouTube", string.Empty, "/Assets/DataImages/1456185574_18-youtub.png", "YouTubeListPage");
			AddNode(menuNodeSocial.Nodes, "Instagram", string.Empty, "/Assets/DataImages/1456185583_38-instag.png", "InstagramListPage");
			Nodes.Add(menuNodeSocial);
			AddNode(Nodes, "Discography", "\ue189", string.Empty, "DiscographyListPage", true);			
			AddNode(Nodes, "Discography Tracks", "\ue1d3", string.Empty, "DiscographyTracksListPage", false);			
			AddNode(Nodes, "Biography", "\ue185", string.Empty, "BiographyListPage", true);			
			AddNode(Nodes, resourceLoader.GetString("NavigationPaneAbout"), "\ue11b", string.Empty, "AboutPage");
			AddNode(Nodes, resourceLoader.GetString("NavigationPanePrivacy"), "\ue1f7", string.Empty, string.Empty, true, string.Empty, "http://1drv.ms/1llJOkM");            
        }

		private void AddNode(ObservableCollection<NavigationNode> nodes, string label, string fontIcon, string image, string pageName, bool isVisible = true, string title = null, string deepLinkUrl = null, bool isSelected = false)
        {
            if (nodes != null && isVisible)
            {
                var node = new ItemNavigationNode
                {
                    Title = title,
                    Label = label,
                    FontIcon = fontIcon,
                    Image = image,
                    IsSelected = isSelected,
                    IsVisible = isVisible,
                    NavigationInfo = NavigationInfo.FromPage(pageName)
                };
                if (!string.IsNullOrEmpty(deepLinkUrl))
                {
                    node.NavigationInfo = new NavigationInfo()
                    {
                        NavigationType = NavigationType.DeepLink,
                        TargetUri = new Uri(deepLinkUrl, UriKind.Absolute)
                    };
                }
                nodes.Add(node);
            }            
        }

        public NavigationNode FindPage(Type pageType)
        {
            return GetAllItemNodes(Nodes).FirstOrDefault(n => n.NavigationInfo.NavigationType == NavigationType.Page && n.NavigationInfo.TargetPage == pageType.Name);
        }

        private IEnumerable<ItemNavigationNode> GetAllItemNodes(IEnumerable<NavigationNode> nodes)
        {
            foreach (var node in nodes)
            {
                if (node is ItemNavigationNode)
                {
                    yield return node as ItemNavigationNode;
                }
                else if(node is GroupNavigationNode)
                {
                    var gNode = node as GroupNavigationNode;

                    foreach (var innerNode in GetAllItemNodes(gNode.Nodes))
                    {
                        yield return innerNode;
                    }
                }
            }
        }
    }
}
