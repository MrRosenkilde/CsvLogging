using System;
using System.Linq;

namespace CsvLogging.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
    public class LoggingModelAttribute : Attribute
    {
        public readonly Type LoggingModelType;
        public LoggingModelAttribute(Type loggingModelType)
        {
            LoggingModelType = loggingModelType;
        }
    }
}
