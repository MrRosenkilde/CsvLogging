using DataModels;
using DataModels.Logging;
using DataModels.Logging.Attributes;
using Logic.Logging;
using NUnit.Framework;
using NunitTests.Logging.LoggingTestCases;
using System;
using System.Linq;

namespace NunitTests
{
    public class CsvLineTesting
    {
        CsvConfig defaultConfig = new CsvConfig(';');
        string Line(object obj) { 

            var line = CsvFunctions.ToCsvLine(obj, defaultConfig);
            Console.WriteLine(line);
            return line;
        }
        [Test]
        public void PrimitiveValuesTest() 
        {
            var line = Line(new IntClass());
            Assert.AreEqual("3", line);
            var line2 = Line(3);
            Assert.AreEqual("3", line2);
            var line3 = Line("abcdefgh");
            Assert.AreEqual("abcdefgh", line3);
        }
        [Test]
        public void TestNestedClasses()
        {
            var c = new TestNestedClassAndStruct()
            {
                PublicClassProperty = new NestedClass
                {
                    nestedPublicClassField = 1,
                    NestedPublicClassProperty = 2
                },
                PublicStructProperty = new NestedStruct
                {
                    publicNestedStructField = 3,
                    PublicNestedStructProperty = 4
                }
            };
            var line = Line(c);
            var header = CsvFunctions.ToCsvHeader(c.GetType(), defaultConfig);
            Console.WriteLine(header);
            var lineTerms = line.Split(defaultConfig.delimiter);
            var d = defaultConfig.delimiter;
            Assert.AreEqual($"2{d}1{d}4{d}3", line);
        }

        [Test]
        public void LogByValueTest() 
        {
            var c = new LogByValueTest()
            {
                dateTime = DateTime.Now, // is logged by value
                point = new ReadonlyPoint3D(1, 2, 3) // is logged by individual fields or properties
            };
            var line = Line(c);
            var d = defaultConfig.delimiter;
            var p = c.point;
            Assert.AreEqual($"{c.dateTime}{d}{p.x}{d}{p.y}{d}{p.z}{d}{p.id}", line);
        }
        [Test]
        public void DontLogAttributeTest() 
        {
            var c = new TestDontLogAttribute();
            var line = Line(c);
            var loggedValuesCount = line.Split(defaultConfig.delimiter).Length;
            var propertyCount = c.GetType().GetProperties().Length;
            var dontLogCount =
                c.GetType()
                .GetProperties()
                .Where(x => Attribute.IsDefined(x, typeof(DontLogAttribute)))
                .Count();
            Assert.AreEqual(propertyCount - dontLogCount, loggedValuesCount);
        }
        [Test]
        public void LoggingModelTest ()
        {
            var c = new POCO();
            var actualLine = Line(c);
            var expectedLine = Line(new POCOLoggingModel());
            Assert.AreEqual(expectedLine, actualLine);
        }
        [Test]
        public void LoggingModelTestWithValues()
        {
            var s0 = "This string should be logged";
            var s1 = "This string should also be logged";
            var c = new POCO() 
            {
                publicStringField = s0,
                PublicStringProperty = s1
            };
            var line = Line(c);
            Assert.IsTrue(line.Contains(s0) && line.Contains(s1));
        }
        [Test]
        public void LoggingModelWithConstructor() 
        {
            var c = new LoggingModelWithConstructorUsed();
            var line = Line(c);
            var attr = Attribute.GetCustomAttribute(
                c.GetType(),
                typeof(LoggingModelAttribute)
            ) as LoggingModelAttribute;
            var expected = (
                attr.NewModelInstance(c)
                as LoggingModelWithConstructor
            ).SetWithConstructorParameter;

            Assert.AreEqual(expected, line);
        }

        [Test]
        public void DontLogPrivateFieldsTest() 
        {
            var c = new PrivateAndPublicFieldTest();
            var line = Line(c);
            var d = defaultConfig.delimiter;
            Assert.AreEqual($"0{d}0", line);
        }

        [Test]
        public void CsvFormatGetsInvokedIfDefined() 
        {
            var c = new CsvSimpleValueFormatTest() {
                FormattedDouble = 3.001D
            };
            var line = Line(c);
            var expected = c.FormattedDouble.ToString("f3");
            Assert.AreEqual(expected, line);
        }

        private class IntClass 
        {
            public int i = 3;
        }
    }
}
