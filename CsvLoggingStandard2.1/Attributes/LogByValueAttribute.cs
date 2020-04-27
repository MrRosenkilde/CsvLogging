using System;
using System.Collections.Generic;
using System.Text;

namespace CsvLogging.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class LogByValueAttribute : Attribute
    {
    }
}
