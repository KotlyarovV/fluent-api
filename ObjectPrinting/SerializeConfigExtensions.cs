using System;
using System.Globalization;


namespace ObjectPrinting
{
    public static class SerializeCongigExtension
    {
        public static PrintingConfig<T> Using<T>(this SerializeConfig<T, double> config, CultureInfo info)
        {
            string DoubleToString(double d) => d.ToString(info);

            config.PrintingConfig.SpecialCultures.Add(typeof(double), (Func<double, string>) DoubleToString);
            return ((ISerializeConfig<T, double>) config).PrintingConfig;
        }
    }
}
