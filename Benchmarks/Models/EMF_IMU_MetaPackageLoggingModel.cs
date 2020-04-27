using CsvLogging.Attributes;

namespace Benchmarks.Models
{
	public readonly struct EMF_IMU_MetaPackageLoggingModel
    {
		public readonly EMFPackage emf;
		public readonly IMUPackage imu;
		[CsvName("Frame ID")]
		public readonly uint frameID;
		[CsvName("Sensor ID")]
		public readonly byte sensorId;

		public EMF_IMU_MetaPackageLoggingModel(EMFPackage emf, IMUPackage imu, uint frameID, byte sensorId)
		{
			this.emf = emf;
			this.imu = imu;
			this.frameID = frameID;
			this.sensorId = sensorId;
		}
	}
}
