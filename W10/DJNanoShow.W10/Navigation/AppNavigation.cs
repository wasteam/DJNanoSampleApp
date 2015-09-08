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
                FontIcon = "\ue10f",
                IsSelected = true,
                NavigationInfo = NavigationInfo.FromPage("HomePage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "Tours",
                FontIcon = "\ue709",
                NavigationInfo = NavigationInfo.FromPage("ToursListPage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "Videos",
                FontIcon = "\ue102",
                NavigationInfo = NavigationInfo.FromPage("VideosListPage")
            });

            Nodes.Add(new GroupNavigationNode
            {
                Label = "Social",
                FontIcon = "\ue10c",
                Visibility = Visibility.Visible,
                Nodes = new ObservableCollection<NavigationNode>()
                {
                    new ItemNavigationNode
                    {
                        Label = "Instagram",
                        Image = "/Assets/DataImages/InstagramIcon.png",
                        NavigationInfo = NavigationInfo.FromPage("InstagramListPage")
                    },
                    new ItemNavigationNode
                    {
                        Label = "YouTube",
                        Image = "/Assets/DataImages/YouTubeIcon.png",
                        NavigationInfo = NavigationInfo.FromPage("YouTubeListPage")
                    },
                    new ItemNavigationNode
                    {
                        Label = "Facebook",
                        Image = "/Assets/DataImages/FacebookIcon.png",
                        NavigationInfo = NavigationInfo.FromPage("FacebookListPage")
                    },
                    new ItemNavigationNode
                    {
                        Label = "Twitter",
                        Image = "/Assets/DataImages/TwitterIcon.png",
                        NavigationInfo = NavigationInfo.FromPage("TwitterListPage")
                    },
                }
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "Discography",
                FontIcon = "\ue958",
                NavigationInfo = NavigationInfo.FromPage("DiscographyListPage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "Biography",
                FontIcon = "\ue181",
                NavigationInfo = NavigationInfo.FromPage("BiographyListPage")
            });

            Nodes.Add(new ItemNavigationNode
            {
                Label = "About",
                FontIcon = "\ue11b",
                NavigationInfo = NavigationInfo.FromPage("AboutPage")
            });
            Nodes.Add(new ItemNavigationNode
            {
                Label = "Privacy terms",
                FontIcon = "\ue1F6",
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
