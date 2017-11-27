
namespace ObjectPrinting
{
    public static class SerializePropertyConfigExtension
    {
        /// <summary>
        /// Trim string values
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="config">configuration with used string value</param>
        /// <param name="stringLenght">length of string after trimming</param>
        /// <returns></returns>
        public static PrintingConfig<T> Trim<T>(this SerializePropertyConfig<T, string> config, int stringLenght)
        {
            config.PrintingConfig.TrimmedProperties.Add(config.UsingPropertyInfo, stringLenght);
            return ((ISerializeConfig<T, string>)config).PrintingConfig;
        }
    }
}
