using CsvLogging.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NunitTests.Logging.LoggingTestCases
{
    [LoggingModel(
        typeof(LoggingModelWithConstructor)
    )]
    public class LoggingModelWithConstructorUsed
    {
        public string Parameter1 = "string Constructor Parameter";
    }
}
