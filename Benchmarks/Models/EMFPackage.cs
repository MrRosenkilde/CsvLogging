using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarks.Models
{
    public readonly struct EMFPackage
    {
        public readonly float x, y, z, qx, qy, qz, qw;
        public readonly byte status, sourceId;
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
		internal string ToCsvString()
			=> $"{x};{y};{z};{qx};{qy};{qz};{qw};{status};{sourceId}";
	}
}
