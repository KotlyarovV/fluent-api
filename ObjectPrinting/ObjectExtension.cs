using System;


namespace ObjectPrinting
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Serialize object
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="obj">this object</param>
        /// <returns>serializarion in string</returns>
        public static string Serialize<T>(this T obj)
        {
            var printer = ObjectPrinter.For<T>();
            return printer.PrintToString(obj);
        }

        /// <summary>
        /// Serialize object with configuration
        /// </summary>
        /// <typeparam name="T">object type</typeparam>
        /// <param name="obj">this object</param>
        /// <param name="configuratePrinter">function wchich include and configurate serializator</param>
        /// <returns>serializarion in string</returns>
        public static string Serialize<T>(this T obj, Action<PrintingConfig<T>> configuratePrinter)
        {
            var printer = ObjectPrinter.For<T>();
            configuratePrinter(printer);
            return printer.PrintToString(obj);
        }
    }
}
