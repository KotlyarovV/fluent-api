using System;
using System.Globalization;
using NUnit.Framework;

namespace ObjectPrinting.Tests
{
	[TestFixture]
	public class ObjectPrinterAcceptanceTests
	{

		[Test]
		public void Demo()
		{
			var person = new Person { Name = "Alex", Age = 19, Height = 188.5, BirthYear = 1990};

		    var printer = ObjectPrinter.For<Person>()
		        .ExcludeType<Guid>()
		        //1. Исключить из сериализации свойства определенного типа
		        .Printing<int>().Using(p => p.ToString() + "рррр")
		        //2. Указать альтернативный способ сериализации для определенного типа
		        .ForNumericType<double>().Using(CultureInfo.CurrentCulture)
		        //3. Для числовых типов указать культуру
		        .Printing(x => x.Age).Using(x => (x + 100).ToString())
		        //4. Настроить сериализацию конкретного свойства
		        .Printing(x => x.Name).Trim(3)
				//5. Настроить обрезание строковых свойств (метод должен быть виден только для строковых свойств)
				.ExcludeProperty(x => x.Height);
                //6. Исключить из сериализации конкретного свойства
                
            string personSerialized = printer.PrintToString(person);
            Console.WriteLine(personSerialized);
		    Console.WriteLine(person.Serialize());
		    Console.WriteLine(person.Serialize(p => p.ExcludeType<int>()));
		    //7. Синтаксический сахар в виде метода расширения, сериализующего по-умолчанию		
		    //8. ...с конфигурированием
		}
	}
}