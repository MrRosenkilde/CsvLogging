using DataModels;
using DataModels.Logging.Attributes;
using System;

namespace NunitTests.Logging.LoggingTestCases
{
    internal class LogByValueTest
	{
		[LogByValue]
		public DateTime dateTime = DateTime.Now;
		public ReadonlyPoint3D point = new ReadonlyPoint3D(100, 200, 300);
	}
}
