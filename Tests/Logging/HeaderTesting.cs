using DataModels.Logging;
using DataModels.Logging.Attributes;
using Logic.Logging;
using Logic.Logging.Extensions;
using NUnit.Framework;
using NunitTests.Logging.LoggingTestCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NunitTests
{
	public class HeaderTesting
	{
		private CsvConfig defaultConfig = new CsvConfig(';');
		private string Header(Type t) => CsvFunctions.ToCsvHeader(t, defaultConfig);
		private bool HasAttribute(Type type, Type attributeType)
			=> Attribute.IsDefined(type, attributeType);
		private IEnumerable<MemberInfo> PublicFieldAndProperties(Type t)
			=> t.GetMembers()
			.Where(x =>
				x.MemberType == MemberTypes.Property
				|| x.MemberType == MemberTypes.Field
			);
		private string ErrorMessage(string header, string name)
			=> $"Header: {header}\n should have contained: {name}";


		[Test]
		public void PrivatePublicFieldsTest()
		{
			var t = typeof(PrivateAndPublicFieldTest);
			var header = Header(t);
			foreach(var p in PublicFieldAndProperties(t))
				Assert.IsTrue(
					header.Contains(p.Name),
					ErrorMessage(header,p.Name)
				);
		}

		

		[Test]
		public void CsvNameIsUsed()
		{
			var t = typeof(TestCsvNameAttribute);
			var header = Header(t);
			var attrName = 
				(
					Attribute.GetCustomAttribute(
						t.GetProperties().FirstOrDefault(x => 
							Attribute.IsDefined(x, typeof(CsvNameAttribute))
						), 
						typeof(CsvNameAttribute)
					) as CsvNameAttribute
				).Name;
			Assert.AreEqual(attrName, header, ErrorMessage(header, attrName));
		}

		internal class TestCsvNameAttribute
		{
			[CsvName("This name is in header")]
			public int NotDisplayed { get; set; }
		}

		[Test]
		public void ContainedClassesShouldHavePropertiesNested()
		{
			var header = Header(typeof(TestNestedClassAndStruct));
			foreach (var p in PublicFieldAndProperties(typeof(NestedClass)))
				Assert.IsTrue(
					header.Contains(p.Name),
					ErrorMessage(header, p.Name)
				);

			foreach (var p in PublicFieldAndProperties(typeof(NestedStruct)))
				Assert.IsTrue(
					header.Contains(p.Name),
					ErrorMessage(header, p.Name)
				);
			Console.WriteLine(header);
		}
		
		[Test]
		public void DontLogAttributeExcludesLogging()
		{
			var t = typeof(TestDontLogAttribute);
			var header = Header(t);
			var shouldNotBeLogged = PublicFieldAndProperties(t)
				.Where(x => Attribute.IsDefined(
							element: x,
							attributeType: typeof(DontLogAttribute)
						)
				);
			var headerWords = header.Split(defaultConfig.delimiter);
			foreach (var m in shouldNotBeLogged) { 
				Assert.IsFalse(
					condition: headerWords.Contains(m.Name),
					message: $"{headerWords} should not contain {m.Name}"
				);
				if ( m.TryConvertToPropertyOrFieldType(out Type nestedType))
					if(!nestedType.CustomIsPrimitive())
						foreach (var mn in PublicFieldAndProperties((m as PropertyInfo).PropertyType))
							Assert.IsFalse(
								condition: headerWords.Contains(mn.Name),
								message: $"{headerWords} should not contain {mn.Name}"
							);
			}
			var ins = new TestDontLogAttribute();
			Console.WriteLine(header);
		}

		
		[Test]
		public void LoggingModelIsUsedWhenSpecified()
		{
			var t = typeof(POCO);
			var header = Header(t);
			var loggingModel = Header(typeof(POCOLoggingModel));
			Assert.AreEqual(
				expected: loggingModel,
				actual: header,
				message: $"{loggingModel}\n and {header} aren't equal"
			);
		}

		[Test]
		public void StructWithLogByValueAttributeShouldHaveTheirNameInHeader()
		{
			var t = typeof(LogByValueTest);
			var header = Header(t);
			Assert.IsTrue(header.Contains("dateTime"));
			Assert.IsTrue(header.Contains("x"));
			Assert.IsTrue(header.Contains("y"));
			Assert.IsTrue(header.Contains("z"));
		}
		
    }
}
