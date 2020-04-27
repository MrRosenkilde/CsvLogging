using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests.TestData
{
	public readonly struct ReadonlyPoint3D
	{
		public readonly float x;
		public readonly float y;
		public readonly float z;


		public ReadonlyPoint3D(float x, float y, float z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public override string ToString()
			=> $"[x: {x};y: {y};z: {z};]";
		public static float operator * (ReadonlyPoint3D left, ReadonlyPoint3D right)
			=> left.x * right.x + left.y * right.y + left.z * right.z;

	}
}
