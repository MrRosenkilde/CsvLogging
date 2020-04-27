using System;
using System.Collections.Generic;
using System.Text;

namespace CsvLogging.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CsvFormatAttribute  : Attribute
    {
        public string nameOfFormatFunction { get; set; }
        public CsvFormatAttribute(string methodName) 
        {
            nameOfFormatFunction = methodName;
        }
    }
}
