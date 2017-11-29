namespace ObjectPrinting
{
    public class ObjectPrinter
	{
        /// <summary>
        /// Configurate serializator for type
        /// </summary>
        /// <typeparam name="T">type for serialization</typeparam>
        /// <returns>printing configuration</returns>
	    public static PrintingConfig<T> For<T>()
	    {
            return new PrintingConfig<T>();
        }
	}
}