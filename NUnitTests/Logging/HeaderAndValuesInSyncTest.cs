using CsvLogging;
using DataModels.AmfitrackPackages;
using NUnit.Framework;
using System;
using TestData;

namespace NunitTests.Logging
{
	public class HeaderAndValuesInSyncTest
    {
        [Test]
        public void Test() 
        {
			var emf = new EMFPackage(1, 2, 3, 4, 5, 6, 7, 8, 9);
			var imu = new IMUPackage(10, 11, 12, 13, 14, 15);
			var package = new EMF_IMU_MetaPackage(emf, imu, 0, 0, 0, 1, 34);
			var config = new CsvConfig(';');
			Console.WriteLine(
				CsvFunctions
					.ToHeader(
						type: typeof(EMF_IMU_MetaPackage),
						config: config
					)
			); ;
			Console.WriteLine(
				CsvFunctions
					.ToLine(
						baseObject: package,
						config: new CsvConfig(';')
					)
			);
		}
    }
}
