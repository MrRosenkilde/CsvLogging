using System;
using System.Collections.Generic;
using System.Text;

namespace CsvLogging.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
    public class DontLogAttribute : Attribute
    {
    }
}
