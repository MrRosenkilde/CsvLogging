using CsvLogging.Attributes;
using System;
using System.Text;
using TestData.Models;

namespace DataModels.AmfitrackPackages
{
	[LoggingModel(
		typeof(EMFPackageLoggingModel)
	)]
	public readonly struct EMFPackage
	{
	
		public readonly float x;
		public readonly float y;
		public readonly float z;
		public readonly float qx;
		public readonly float qy;
		public readonly float qz;
		public readonly float qw;
		public readonly byte status;
		public readonly byte sourceId;
		public EMFPackage(in float x, in float y, in float z, in float qx, in float qy, in float qz, in float qw, in byte status, in byte sourceId)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.qx = qx;
			this.qy = qy;
			this.qz = qz;
			this.qw = qw;
			this.status = status;
			this.sourceId = sourceId;

		}
		public override string ToString()
		{
			var sb = new StringBuilder();
			foreach (var field in GetType().GetFields())
				sb.Append(field.ToString() + ";");
			return sb.ToString();
		}
	}
}
