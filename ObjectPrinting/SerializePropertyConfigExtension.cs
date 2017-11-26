
namespace ObjectPrinting
{
    public static class SerializePropertyConfigExtension
    {
        public static PrintingConfig<T> Trim<T>(this SerializePropertyConfig<T, string> config, int stringLenght)
        {
            config.PrintingConfig.TrimmedProperties.Add(config.UsingPropertyInfo, stringLenght);
            return ((ISerializeConfig<T, string>)config).PrintingConfig;
        }
    }
}
