using System;
using System.Collections.Generic;
using System.Text;

namespace CsvLogging
{
    public interface ICsvLoggable
    {
        public string ToCsvHeader();
        public string ToCsvLine();
    }
}
