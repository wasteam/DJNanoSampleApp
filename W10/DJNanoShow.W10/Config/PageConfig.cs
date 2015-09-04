using System;
using System.Collections.Generic;
using AppStudio.Common.Navigation;
using AppStudio.DataProviders;
using DJNanoShow.ViewModels;

namespace DJNanoShow.Config
{
    public abstract class PageConfigBase
    {
        public string Title { get; set; }
    }

    public class ListPageConfig<T> : PageConfigBase where T : SchemaBase
    {
        public Action<ItemViewModel, T> LayoutBindings { get; set; }

        public Func<T, NavigationInfo> NavigationInfo { get; set; }
    }

    public class DetailPageConfig<T> : PageConfigBase where T : SchemaBase
    {
        public List<Action<ItemViewModel, T>> LayoutBindings { get; set; }
        public List<ActionConfig<T>> Actions { get; set; }

    }
}
