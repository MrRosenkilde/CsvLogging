using NUnitTests.TestData;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTests.Logging.LoggingTestCases
{
    public class CollectionTestCase
    {
        public ReadonlyPoint3D[] ints = new ReadonlyPoint3D[] 
        { 
            new ReadonlyPoint3D(0,0,0),
            new ReadonlyPoint3D(100, 100, 100),
            new ReadonlyPoint3D(200, 200, 200),
        };
    }
}
