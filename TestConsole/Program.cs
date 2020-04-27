using NUnitTests.Logging.LoggingTestCases;
using System;
using System.Collections;
using System.Reflection;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new CollectionTestCase();
            var type = obj.GetType();
            var member = type.GetMember("ints")[0];
            var val = (member as FieldInfo).GetValue(obj) as IEnumerable;
            foreach (var collectionElement in val)
                Console.WriteLine(collectionElement);

        }
    }
}
