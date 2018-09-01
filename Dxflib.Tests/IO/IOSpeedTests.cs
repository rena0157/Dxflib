using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests.IO
{
    /// <summary>
    /// This class is used to test different speeds of
    /// parsing techniques
    /// </summary>
    [TestClass]
    public class IOSpeedTests
    {
        /// <summary>
        /// This parsing technique uses raw strings from the
        /// Dxf file and tries to parse them using a switch statement
        /// </summary>
        public void ParsingStringsInSwitch()
        {
            var numberOfObjects = 100000000;

            var testSString = " 71";

            // Allocate the array
            var testList1 = new List<string>(numberOfObjects);

            // Allocate the string array
            for (var i = 0; i < numberOfObjects; ++i)
                testList1.Add(testSString);


            Stopwatch test = new Stopwatch();
            test.Start();
            var hits = 0;
            foreach ( var str in testList1 )
            {
                switch ( str )
                {
                    case " 34":
                        hits += 1;
                        continue;
                    case " 74":
                        hits += 1;
                        continue;
                    case " 71":
                        hits += 1;
                        continue;
                    default:
                        continue;
                }
            }
            test.Stop();
            var time = test.ElapsedMilliseconds;

            Debug.WriteLine($"{numberOfObjects} iterations took: {time}ms");
        }

        /// <summary>
        /// This parsing technique uses the same string as above but
        /// is converted to an int before it is used in the switch
        /// statement
        /// </summary>
        public void ParsingIntsInSwitch()
        {
            var numberOfObjects = 100000000;

            var testSString = " 71";

            // Allocate the array
            var testList1 = new List<string>(numberOfObjects);

            // Allocate the string array
            for (var i = 0; i < numberOfObjects; ++i)
                testList1.Add(testSString);


            Stopwatch test = new Stopwatch();
            test.Start();
            var hits = 0;
            foreach ( var str in testList1 )
            {
                var parsedInt = int.Parse(str);
                switch ( parsedInt )
                {
                    case 34:
                        hits += 1;
                        continue;
                    case 74:
                        hits += 1;
                        continue;
                    case 71:
                        hits += 1;
                        continue;
                    default:
                        continue;
                }
            }
            test.Stop();
            var time = test.ElapsedMilliseconds;

            Debug.WriteLine($"{numberOfObjects} iterations took: {time}ms");
        }
    }
}
