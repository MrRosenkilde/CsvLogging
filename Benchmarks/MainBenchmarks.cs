using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Benchmarks.Models;
using CsvLogging;
using System;
namespace Benchmarks { 

    public class MainBenchmarks
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<InterfaceImplementation>();
        }
        [MemoryDiagnoser]
        [MinColumn, MaxColumn]
        public class InterfaceImplementation 
        {
            private ClassWithInterfaceOptimization classWithInterfaceImplemented;
            private ClassWithoutInterfaceOptimization classWithoutInterfaceOptimization;
            private ReadonlyPoint3D point;
            private CsvConfig config;
            public InterfaceImplementation() 
            {
                classWithInterfaceImplemented = new ClassWithInterfaceOptimization();
                classWithoutInterfaceOptimization = new ClassWithoutInterfaceOptimization();
                point = new ReadonlyPoint3D(100, 100, 100);
                config = new CsvConfig(';');
            }

            [Benchmark(Baseline = true, Description = "ToLine() function without using interface optimization")]
            public string WithoutOptimization()
                => CsvFunctions.ToLine(classWithoutInterfaceOptimization, config);

            [Benchmark(Description = "ToLine() function with interface optimization")]
            public string WithOptimization()
                => CsvFunctions.ToLine(classWithInterfaceImplemented, config);

            [Benchmark(Description = "Time it would take to write the content of the class")]
            public string JustThePoint()
                => CsvFunctions.ToLine(point, config);
        }

        [MemoryDiagnoser]
        [MinColumn, MaxColumn, IterationsColumn]
        public class ToLineFunction 
        {
            private ReadonlyPoint3D point;
            private Random random;
            private EMFPackageWithReadonlyLoggingModel emfWithReadonlyLoggingModel;
            private EMFPackageWithMutableLoggingModel emfWithMutableLoggingModel;
            private EMFPackage emfPackage;
            private EMF_IMU_MetaPackage emf_imu_metaPackage;
            private float randomFloat() => (float) random.NextDouble() * 1000f;

            private readonly static CsvConfig config = new CsvConfig(';');
            public ToLineFunction() 
            {
                random = new Random(42);
                point = new ReadonlyPoint3D(
                    randomFloat(),
                    randomFloat(),
                    randomFloat()
                );
                emfWithReadonlyLoggingModel = new EMFPackageWithReadonlyLoggingModel(
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    (byte)random.Next(0, 255),
                    (byte)random.Next(0, 255)
                );
                emfWithMutableLoggingModel = new EMFPackageWithMutableLoggingModel(
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    (byte)random.Next(0, 255),
                    (byte)random.Next(0, 255)
                );
                emfPackage = new EMFPackage(
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    randomFloat(),
                    (byte)random.Next(0,255),
                    (byte)random.Next(0,255)
                );
                emf_imu_metaPackage = new EMF_IMU_MetaPackage(
                    emf: emfPackage,
                    imu: new IMUPackage(
                      (short) random.Next(0,30_000),
                      (short) random.Next(0,30_000),
                      (short) random.Next(0,30_000),
                      (short) random.Next(0,30_000),
                      (short) random.Next(0,30_000),
                      (short) random.Next(0,30_000)
                    ),
                    0,0,0,0,0
                ); ;
            }

            [Benchmark(Baseline = true, Description = "the ToString() method of an EMFPackage")]
            public string BaseLine()
                => emfPackage.ToCsvString();

            [Benchmark(Description = "Bechmarks for CsvFunctions.ToLine for a ReadonlyPoint3D instance")]
            public string BenchmarkToLineWithForReadonlyPoint3D()
                => CsvFunctions.ToLine(point, config);

            [Benchmark(Description = "Bechmarks for CsvFunctions.ToLine for a EMFPackage with a ReadonlyLoggingModel")]
            public string BenchmarkToLineForEmfPackageWithReadonlyLoggingModel()
                => CsvFunctions.ToLine(emfWithReadonlyLoggingModel, config);

            [Benchmark(Description = "Bechmarks for CsvFunctions.ToLine for a EMFPackage with a Mutable LoggingModel")]
            public string BenchmarkToLineForEmfPackageWithMutableLoggingModel()
                => CsvFunctions.ToLine(emfWithMutableLoggingModel, config);

            [Benchmark(Description = "Bechmarks for CsvFunctions.ToLine for a EMFPackage with no logging model")]
            public string BenchmarkToLineForEmfPackage()
                => CsvFunctions.ToLine(emfPackage, config);

            [Benchmark(Description = "Bechmarks for CsvFunctions.ToLine for a EMF_IMU_Metapackage instance")]
            public string BenchmarkToLineForEMF_IMU_MetaPackage()
                => CsvFunctions.ToLine(emf_imu_metaPackage, config);

        }
    }
}