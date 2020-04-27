using CsvLogging;
using DataModels;
using NUnit.Framework;
using NUnitTests.TestData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NunitTests
{
    public class LoggingExtensionsTesting
    {

        [Test]
        public void IsCollectionTesting() 
        {
            var typeArr=  new Type[]
            {
                typeof(int[]),
                typeof(IList<int>),
                typeof(List<int>),
                typeof(ObservableCollection<int>),
                typeof(IEnumerable<int>)
            };
            foreach(var t in typeArr)
                Assert.IsTrue(
                    condition: t.IsCollection(),
                    message: $"{t} is not a collection, but should be"
                );
        }
        [Test]
        public void TestGetCollectionType() {
            var testClass = new TestGetCollectionTypeClass();
            var testCases = new (string name, Type expected)[]{
                (nameof(testClass.IntArray), typeof(int)),
                (nameof(testClass.ListOfStrings), typeof(string)),
                (nameof(testClass.ReadonlyPoint3D), typeof(ReadonlyPoint3D)),
                (nameof(testClass.ClassCollection), typeof(TestGetCollectionTypeClass)),
                (nameof(testClass.TupleIEnumerable), typeof((string,int)) ),
                (nameof(testClass.NotCollection), null),
                (nameof(testClass.StringNotCollection), null),
                (nameof(testClass.Tuple), null)
            };
            Type Actual (string name) { 
                typeof(TestGetCollectionTypeClass)
                    .GetMember(name)[0]
                    .TryGetCollectionType(out Type intArrayElementType);
                return intArrayElementType;
            }
            foreach(var (name, expected) in testCases)
                Assert.AreEqual(expected, Actual(name));
            
        }
        private class TestGetCollectionTypeClass
        {
            public int[] IntArray { get; set; }
            public List<string> ListOfStrings { get; set; }
            public IList<ReadonlyPoint3D> ReadonlyPoint3D { get; set; }
            public ICollection<TestGetCollectionTypeClass> ClassCollection { get; set; }
            public IEnumerable<(string, int)> TupleIEnumerable { get; set; }
            public int NotCollection { get; set; }
            public string StringNotCollection { get; set; }
            public Tuple<int, int> Tuple { get; set; }
        }
    }
}
