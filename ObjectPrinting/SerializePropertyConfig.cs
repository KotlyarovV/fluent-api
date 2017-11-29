using System;
using System.Reflection;

namespace ObjectPrinting
{
    public class SerializePropertyConfig<TOwner, TType> : PrintingConfig<TOwner>, ISerializeConfig<TOwner, TType>
    {
        internal readonly PrintingConfig<TOwner> PrintingConfig;

        internal readonly PropertyInfo UsingPropertyInfo;
        
        public SerializePropertyConfig(PrintingConfig<TOwner> printingConfig, PropertyInfo property)
        {
            PrintingConfig = printingConfig;
            UsingPropertyInfo = property;
        }

        /// <summary>
        /// set serialization function for property
        /// </summary>
        /// <param name="func">serialization function</param>
        /// <returns>configured printingconfig</returns>
        public PrintingConfig<TOwner> Using(Func<TType, string> func)
        {
            PrintingConfig.SpecialProperties.Add(UsingPropertyInfo, func);
            return PrintingConfig;
        }

        PrintingConfig<TOwner> ISerializeConfig<TOwner, TType>.PrintingConfig => PrintingConfig;
    }

}
