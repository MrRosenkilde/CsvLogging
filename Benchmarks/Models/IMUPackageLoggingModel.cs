using CsvLogging.Attributes;

namespace Benchmarks.Models
{
	public struct IMUPackageLoggingModel
	{
		[CsvName("Acceleration X")]
		public readonly short accelerationX;
		[CsvName("Acceleration Y")]
		public readonly short accelerationY;
		[CsvName("Acceleration Z")]
		public readonly short accelerationZ;
		[CsvName("Angular Velocity X")]
		public readonly short angularVelocityX;
		[CsvName("Angular Velocity Y")]
		public readonly short angularVelocityY;
		[CsvName("Angular Velocity Z")]
		public readonly short angularVelocityZ;

		public IMUPackageLoggingModel( short accelerationX,  short accelerationY,  short accelerationZ,  short angularVelocityX,  short angularVelocityY,  short angularVelocityZ)
		{
			this.accelerationX = accelerationX;
			this.accelerationY = accelerationY;
			this.accelerationZ = accelerationZ;
			this.angularVelocityX = angularVelocityX;
			this.angularVelocityY = angularVelocityY;
			this.angularVelocityZ = angularVelocityZ;
		}
	}
}
