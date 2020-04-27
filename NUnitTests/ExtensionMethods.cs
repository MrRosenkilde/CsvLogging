using System;
using System.Collections.Generic;
using System.Text;

namespace NunitTests
{
	public static class ExtensionMethods
	{
		public static float NextFloat(this Random r, int scalar = 1) => (float)r.NextDouble() * scalar;
	}
}
