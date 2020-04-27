using CsvLogging;
using CsvLogging.Attributes;
using DataModels;
using NUnit.Framework;
using NunitTests.Logging.LoggingTestCases;
using NUnitTests.Logging.LoggingTestCases;
using NUnitTests.TestData;
using System;
using System.Linq;

namespace NunitTests
{
    public class CsvLineTesting
    {
        CsvConfig defaultConfig = new CsvConfig(';');
        string Line(object obj, CsvConfig config) { 

            var line = CsvFunctions.ToLine(obj, config);
            Console.WriteLine(line);
            return line;
        }
        string Line(object obj)
            => Line(obj, defaultConfig);
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
            var header = CsvFunctions.ToHeader(c.GetType(), defaultConfig);
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
            Assert.AreEqual($"{c.dateTime}{d}{p.x}{d}{p.y}{d}{p.z}", line);
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
            Assert.IsTrue(
                line.Contains(s0) && line.Contains(s1),
                message: $"line didn't contain:\n{s0} \nand {s1}"
            );
        }

        [Test]
        public void DontLogPrivateFieldsTest() 
        {
            var c = new PrivateAndPublicFieldTest();
            var d = defaultConfig.delimiter;
            var line = Line(c);
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
        [Test]
        public void TestValueTuple() 
        {
            ValueTuple<ReadonlyPoint3D, ReadonlyPoint3D> c 
                = (new ReadonlyPoint3D(1, 2, 3), new ReadonlyPoint3D(4, 5, 6));
            var line = Line(c);
            var d = defaultConfig.delimiter;
            var expected = $"1{d}2{d}3{d}4{d}5{d}6";
            Assert.AreEqual(expected, line);

        }
        [Test]
        public void TestDelimiter()
        {
            int x = 1, y = 2, z = 3;
            var config = new CsvConfig('!');
            var line = CsvFunctions.ToLine((x,y,z),config);
            Console.WriteLine(line);
            Assert.AreEqual("1!2!3", line);
        }
        [Test]
        public void TestCachedValuesIsByReference() 
        {
            var person = new Person("Jens", "Petersen");
            var line0 = Line(person);
            person.FirstName = "Henrik";
            var line1 = Line(person);
            Assert.AreNotEqual(
                line0,
                line1,
                message: $"{line0}\nand {line1} was not supposed to be equal"
            );
        }

        [Test]
        public void TestInterfaceImplementation() 
        {
            var c = new ICsvLoggableTest();
            var line = Line(c);
            var expected = c.ToCsvLine();
            Assert.AreEqual(expected, line);
        }

        [Test]
        public void InlineCollectionStrategyWritesCollectionInline() 
        {
            var _case = new CollectionTestCase();
            var config = new CsvConfig(';', CollectionStrategy.Inline);
            var line = Line(_case, config);
            var expected = "[{0;0;0};{100;100;100};{200;200;200}]";
            Assert.AreEqual(expected, line);
        }
        
        private class IntClass 
        {
            public int i = 3;
        }
    }
}
