using CsvLogging.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarks.Models
{
	public struct EMFPackageMutableLoggingModel
	{
		public string Format(float x) => x.ToString("f3");

		[CsvName("EMF X")]
		[CsvFormat(nameof(Format))]
		public float x;
		[CsvName("EMF Y")]
		[CsvFormat(nameof(Format))]
		public float y;
		[CsvName("EMF Z")]
		[CsvFormat(nameof(Format))]
		public float z;
		[CsvName("EMF Quaternion X")]
		[CsvFormat(nameof(Format))]
		public float qx;
		[CsvName("EMF Quaternion Y")]
		[CsvFormat(nameof(Format))]
		public  float qy;
		[CsvName("EMF Quaternion Z")]
		[CsvFormat(nameof(Format))]
		public  float qz;
		[CsvName("EMF Quaternion W")]
		[CsvFormat(nameof(Format))]
		public  float qw;
		[CsvName("Source ID")]
		public  byte sourceId;
	}
}
