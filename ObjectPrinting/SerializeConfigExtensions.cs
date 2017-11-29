using System;
using System.Globalization;


namespace ObjectPrinting
{
    public static class SerializeCongigExtension
    {
        /// <summary>
        /// Set culture info for double type
        /// </summary>
        /// <typeparam name="T">object typr</typeparam>
        /// <param name="config">configutaion if doublr type</param>
        /// <param name="info">culture</param>
        /// <returns></returns>
        public static PrintingConfig<T> Using<T>(this SerializeConfig<T, double> config, CultureInfo info)
        {
            string DoubleToString(double d) => d.ToString(info);

            config.PrintingConfig.SpecialCultures.Add(typeof(double), (Func<double, string>) DoubleToString);
            return ((ISerializeConfig<T, double>) config).PrintingConfig;
        }
    }
}
