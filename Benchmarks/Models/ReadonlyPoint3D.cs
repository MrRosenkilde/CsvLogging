using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarks.Models
{
    public readonly struct ReadonlyPoint3D
    {
        public readonly float x, y, z;
        public ReadonlyPoint3D(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
