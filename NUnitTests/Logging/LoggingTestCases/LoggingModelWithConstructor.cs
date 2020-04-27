using CsvLogging.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NunitTests.Logging.LoggingTestCases
{
    public class LoggingModelWithConstructor
    {
        [CsvName("Set From constructor")]
        public readonly string Parameter1;

        public LoggingModelWithConstructor(string s) 
        {
            Parameter1 = s;
        }
    }
}
