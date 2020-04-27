using DataModels.Logging.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NunitTests.Logging.LoggingTestCases
{
    [LoggingModel(
        typeof(LoggingModelWithConstructor),
        new Type[]
        {
            typeof(string)
        },
        new string[]
        {
            nameof(parameter1)
        }
    )]
    public class LoggingModelWithConstructorUsed
    {
        public string parameter1 = "string Constructor Parameter";
    }
}
