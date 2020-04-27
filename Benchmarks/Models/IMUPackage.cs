using CsvLogging.Attributes;
using System.Text;

namespace Benchmarks.Models
{
	[LoggingModel(
		typeof(IMUPackageLoggingModel)
	)]
	public readonly struct IMUPackage
	{
		
		public readonly short accelerationX;
		public readonly short accelerationY;
		public readonly short accelerationZ;
		public readonly short angularVelocityX;
		public readonly short angularVelocityY;
		public readonly short angularVelocityZ;

		public IMUPackage(in short accelerationX, in short accelerationY, in short accelerationZ, in short angularVelocityX, in short angularVelocityY, in short angularVelocityZ)
		{
			this.accelerationX = accelerationX;
			this.accelerationY = accelerationY;
			this.accelerationZ = accelerationZ;
			this.angularVelocityX = angularVelocityX;
			this.angularVelocityY = angularVelocityY;
			this.angularVelocityZ = angularVelocityZ;
		}
		public new string ToString()
		{
			var sb = new StringBuilder();
			foreach (var field in GetType().GetFields())
				sb.Append(field.ToString() + ";");
			return sb.ToString();
		}
	}
}
