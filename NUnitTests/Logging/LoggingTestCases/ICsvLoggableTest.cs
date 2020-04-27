using CsvLogging;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests.Logging.LoggingTestCases
{
    public class ICsvLoggableTest : ICsvLoggable
    {

        public string ToCsvHeader()
        {
            return "Header as specified in interface";
        }

        public string ToCsvLine()
        {
            return "line specified from interface";
        }
    }
}
