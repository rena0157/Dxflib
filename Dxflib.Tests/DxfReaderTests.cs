// Dxflib.Tests
// UnitTest1.cs
// 
// ============================================================
// 
// Created: 2018-08-03
// Last Updated: 2018-08-03-7:27 PM
// By: Adam Renaud
// 
// ============================================================
// 
// Purpose:

using System.Diagnostics;
using Dxflib.DxfStream;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests
{
    [TestClass]
    public class DxfReaderTests
    {
        [TestMethod]
        public void ReadFile_ThrowExceptionDueToFileNotExisting()
        {
            var fileExists = true;

            try
            {
                // ReSharper disable once UnusedVariable
                var testDxfFile = new DxfFile(@"ThisPathDoesNotExist.txt");
            }
            catch (DxfStreamException e)
            {
                Debug.WriteLine(e.Message + ": TEST PASSED");
                fileExists = false;
            }
            finally
            {
                if (fileExists) Debug.WriteLine("Either this file does exist or the exception is not working properly");

                Assert.IsFalse(fileExists);
            }
        }
    }
}