using System;


namespace ObjectPrinting
{
    /// <summary>
    /// Expand Printing config 
    /// </summary>
    /// <typeparam name="TOwner"></typeparam>
    /// <typeparam name="TType"></typeparam>
    public class SerializeConfig<TOwner, TType> : PrintingConfig<TOwner>, ISerializeConfig<TOwner, TType>
    {
        internal readonly PrintingConfig<TOwner> PrintingConfig;

        public SerializeConfig(PrintingConfig<TOwner> printingConfig)
        {
            PrintingConfig = printingConfig;
        }

        /// <summary>
        /// set serialization function for type
        /// </summary>
        /// <param name="func">serialization finction</param>
        /// <returns>printconfig with new rule for TType</returns>
        public PrintingConfig<TOwner> Using(Func<TType, string> func)
        {
            PrintingConfig.TypeRules.Add(typeof(TType), func);
            return PrintingConfig;
        }

        PrintingConfig<TOwner> ISerializeConfig<TOwner, TType> .PrintingConfig => PrintingConfig;
    }

    public interface ISerializeConfig<TOnwer, TPropType>
    {
        PrintingConfig<TOnwer> PrintingConfig { get; }
    }

}
