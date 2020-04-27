using CsvLogging.Attributes;

namespace TestData.Models
{
	public readonly struct EMFPackageLoggingModel
	{
		public string Format(float x) => x.ToString("f3");

		[CsvName("EMF X")]
		[CsvFormat(nameof(Format))]
		public readonly float x;
		[CsvName("EMF Y")]
		[CsvFormat(nameof(Format))]
		public readonly float y;
		[CsvName("EMF Z")]
		[CsvFormat(nameof(Format))]
		public readonly float z;
		[CsvName("EMF Quaternion X")]
		[CsvFormat(nameof(Format))]
		public readonly float qx;
		[CsvName("EMF Quaternion Y")]
		[CsvFormat(nameof(Format))]
		public readonly float qy;
		[CsvName("EMF Quaternion Z")]
		[CsvFormat(nameof(Format))]
		public readonly float qz;
		[CsvName("EMF Quaternion W")]
		[CsvFormat(nameof(Format))]
		public readonly float qw;
		[CsvName("Source ID")]
		public readonly byte sourceId;
		public EMFPackageLoggingModel( float x,  float y,  float z,  float qx,  float qy,  float qz,  float qw,  byte sourceId)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.qx = qx;
			this.qy = qy;
			this.qz = qz;
			this.qw = qw;
			this.sourceId = sourceId;

		}
	}
}
