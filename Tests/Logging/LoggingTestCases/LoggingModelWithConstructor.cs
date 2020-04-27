using System;
using System.Collections.Generic;
using System.Text;

namespace NunitTests.Logging.LoggingTestCases
{
    public class LoggingModelWithConstructor
    {
        public readonly string SetWithConstructorParameter;

        public LoggingModelWithConstructor(string s) 
        {
            SetWithConstructorParameter = s;
        }
    }
}
