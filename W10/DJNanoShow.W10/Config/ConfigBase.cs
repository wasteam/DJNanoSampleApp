using AppStudio.DataProviders;

namespace DJNanoShow.Config
{
    public abstract class ConfigBase<TConfig, TSchema> where TSchema : SchemaBase
    {
        public abstract DataProviderBase<TConfig, TSchema> DataProvider { get; }

        public abstract TConfig Config { get; }

        public string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }
    }
}
