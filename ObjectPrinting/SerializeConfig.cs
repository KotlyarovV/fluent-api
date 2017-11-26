using System;


namespace ObjectPrinting
{
    public class SerializeConfig<TOwner, TType> : PrintingConfig<TOwner>, ISerializeConfig<TOwner, TType>
    {
        internal readonly PrintingConfig<TOwner> PrintingConfig;

        public SerializeConfig(PrintingConfig<TOwner> printingConfig)
        {
            PrintingConfig = printingConfig;
        }

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
