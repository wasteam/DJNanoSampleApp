using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppStudio.DataProviders;
using AppStudio.Uwp.Navigation;

namespace DJNanoShow.Config
{
    public class RelatedContentConfig<TSchema, TMasterSchema> where TSchema : SchemaBase where TMasterSchema : SchemaBase
    {
        public bool NeedsNetwork { get; set; } = true;
        public Func<TMasterSchema, Task<IEnumerable<TSchema>>> LoadDataAsync { get; set; }

        public ListPageConfig<TSchema> ListPage { get; set; }
    }
}
