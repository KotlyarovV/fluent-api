using System;


namespace ObjectPrinting
{
    public static class ObjectExtension
    {
        public static string Serialize<T>(this T obj)
        {
            var printer = ObjectPrinter.For<T>();
            return printer.PrintToString(obj);
        }

        public static string Serialize<T>(this T obj, Action<PrintingConfig<T>> configuratePrinter)
        {
            var printer = ObjectPrinter.For<T>();
            configuratePrinter(printer);
            return printer.PrintToString(obj);
        }
    }
}
