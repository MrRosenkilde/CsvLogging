using CsvLogging.Attributes;
using DataModels.AmfitrackPackages;
using System;
using System.Text;
using TestData.Models;

namespace TestData
{
	[LoggingModel(
		typeof(EMF_IMU_MetaPackageLoggingModel)
	)]
	public readonly struct EMF_IMU_MetaPackage
	{
		
		public readonly EMFPackage emf;
		public readonly IMUPackage imu;
		public readonly ushort samplingTimestamp;
		public readonly sbyte RSSI;
		public readonly byte usbPacketNumber;

		public readonly uint frameID;
		public readonly byte sensorId;

		public EMF_IMU_MetaPackage(in EMFPackage emf,
			in IMUPackage imu,
			ushort samplingTimestamp,
			sbyte RSSI,
			byte usbPacketNumber,
			uint frameID,
			byte sensorId
		){
			this.emf = emf;
			this.imu = imu;
			this.samplingTimestamp = samplingTimestamp;
			this.RSSI = RSSI;
			this.usbPacketNumber = usbPacketNumber;
			this.frameID = frameID;
			this.sensorId = sensorId;
		}
		public override string ToString(){
			var sb = new StringBuilder();
			foreach (var field in GetType().GetFields())
				sb.Append(field.ToString()+";");
			return sb.ToString();
		}

	}
}
