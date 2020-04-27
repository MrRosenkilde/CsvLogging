using CsvLogging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Benchmarks.Models
{
    public class ClassWithInterfaceOptimization : ICsvLoggable
    {
        public ReadonlyPoint3D point = new ReadonlyPoint3D(100,100,100);

        public string ToCsvHeader()
        {
            return "X;Y;Z";
        }

        public string ToCsvLine()
        {
            return $"{point.x};{point.y};{point.z}";
        }
    }
    public class ClassWithoutInterfaceOptimization 
    {
        public ReadonlyPoint3D point = new ReadonlyPoint3D(100, 100, 100);
    }
}
