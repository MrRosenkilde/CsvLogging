using System;
using System.Collections.Generic;
using System.Text;

namespace CsvLogging.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class CsvNameAttribute : Attribute
    {
        public string Name { get; set; }
        public CsvNameAttribute(string name) 
        {
            Name = name;
        }
    }
}
