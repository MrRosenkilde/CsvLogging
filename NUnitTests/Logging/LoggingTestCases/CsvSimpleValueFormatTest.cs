using CsvLogging.Attributes;

namespace NunitTests.Logging.LoggingTestCases
{
    public class CsvSimpleValueFormatTest
    {
        public string FormatDouble(double d) 
            => d.ToString("f3");

        [CsvFormat(nameof(FormatDouble))]
        public double FormattedDouble { get; set; }
    }
}
