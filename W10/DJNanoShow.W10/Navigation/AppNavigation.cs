using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AppStudio.Common.Navigation;
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

            Nodes.Add(new ItemNavigationNode
            {
                Title = @"DJNano Show",
                Label = "Home",
                IsSelected = true,
                NavigationInfo = NavigationInfo.FromPage("HomePage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "Tours",
                NavigationInfo = NavigationInfo.FromPage("ToursListPage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "Videos",
                NavigationInfo = NavigationInfo.FromPage("VideosListPage")
            });

            Nodes.Add(new GroupNavigationNode
            {
                Label = "Social",
                Visibility = Visibility.Visible,
                Nodes = new ObservableCollection<NavigationNode>()
                {
                    new ItemNavigationNode
                    {
                        Label = "Instagram",
                        NavigationInfo = NavigationInfo.FromPage("InstagramListPage")
                    },
                    new ItemNavigationNode
                    {
                        Label = "YouTube",
                        NavigationInfo = NavigationInfo.FromPage("YouTubeListPage")
                    },
                    new ItemNavigationNode
                    {
                        Label = "Facebook",
                        NavigationInfo = NavigationInfo.FromPage("FacebookListPage")
                    },
                    new ItemNavigationNode
                    {
                        Label = "Twitter",
                        NavigationInfo = NavigationInfo.FromPage("TwitterListPage")
                    },
                }
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "Discography",
                NavigationInfo = NavigationInfo.FromPage("DiscographyListPage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "Biography",
                NavigationInfo = NavigationInfo.FromPage("BiographyListPage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "About",
                NavigationInfo = NavigationInfo.FromPage("AboutPage")
            });
            Nodes.Add(new ItemNavigationNode
            {
                Label = "Privacy terms",
                NavigationInfo = new NavigationInfo()
                {
                    NavigationType = NavigationType.DeepLink,
                    TargetUri = new Uri("http://1drv.ms/1llJOkM", UriKind.Absolute)
                }
            });
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
