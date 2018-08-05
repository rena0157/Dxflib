// Dxflib.Tests
// LayerDictionaryTests.cs
// 
// ============================================================
// 
// Created: 2018-08-05
// Last Updated: 2018-08-05-11:41 AM
// By: Adam Renaud
// 
// ============================================================

using System.Collections.Generic;
using Dxflib.AcadEntities;
using Dxflib.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dxflib.Tests
{
    [TestClass]
    public class LayerDictionaryTests
    {
        [TestMethod]
        public void NewLayer_AddingANewLayer()
        {
            // The layer Dictionary
            var layerDictionary = new LayerDictionary();
            // The inital count should be 0
            Assert.IsTrue(layerDictionary.Count == 0);
            // Adding one layer
            layerDictionary.NewLayer("HelloWorld");
            // Now the count should be 1
            Assert.IsTrue(layerDictionary.Count == 1);
        }

        [TestMethod]
        public void ContainsLayer_ShouldContainTheLayerHelloWorld()
        {
            // The layer Dictionary
            var layerDictionary = new LayerDictionary();
            // The inital count should be 0
            Assert.IsTrue(layerDictionary.Count == 0);
            // Adding one layer
            layerDictionary.NewLayer("HelloWorld");
            // Now the count should be 1
            Assert.IsTrue(layerDictionary.Count == 1);
            // The layerDic should contain the layer HelloWorld
            Assert.IsTrue(layerDictionary.ContainsLayer("HelloWorld"));
            Assert.IsFalse(layerDictionary.ContainsLayer("ShouldNotContain"));
        }

        [TestMethod]
        public void GetLayer_ShouldGetHelloWorldLayer()
        {
            // The layer Dictionary
            var layerDictionary = new LayerDictionary();

            // The inital count should be 0
            Assert.IsTrue(layerDictionary.Count == 0);

            // Adding one layer
            layerDictionary.NewLayer("HelloWorld");

            // Now the count should be 1
            Assert.IsTrue(layerDictionary.Count == 1);

            // Get the layer HelloWorld
            var testGetLayer = layerDictionary.GetLayer("HelloWorld");
            Assert.IsTrue(testGetLayer.Name == "HelloWorld");
        }

        [TestMethod]
        [ExpectedException(typeof(LayerDictionaryException))]
        public void GetLayer_LayerDoesNotExist_LayerDictionaryExceptionThrown()
        {
            var layerDictionary = new LayerDictionary();
            layerDictionary.GetLayer("ThisShouldFail");
        }

        [TestMethod]
        public void UpdateDictionaryTest()
        {
            // The test Dictionary
            var testDictionary = new LayerDictionary();

            // TestLine 0
            var testLine0 = new Line(new LineBuffer
            {
                EntityType = EntityTypes.Line,
                handle = "1A",
                LayerName = "TestLayer",
                Thickness = 0,
                X0 = 0,
                X1 = 0,
                Y0 = 1,
                Y1 = 2
            });

            // TestLine1
            var testLine1 = new Line(new LineBuffer
            {
                EntityType = EntityTypes.Line,
                handle = "2A",
                LayerName = "TestLayer",
                Thickness = 0,
                X0 = 0,
                X1 = 0,
                Y0 = 1,
                Y1 = 2
            });

            // Place the entities into a list 
            var entities = new List<Entity> {testLine0, testLine1};

            // Create the Dictionary
            testDictionary.UpdateDictionary(entities);

            // Update Dictionary should have created the layer test Layer
            Assert.IsTrue(testDictionary.ContainsLayer("TestLayer"));
            Assert.IsTrue(testDictionary.Count == 1);
            // Change the layer of the second line
            testLine1.LayerName = "NewLayer";

            // Assert that the new layer was created,
            // The second line is now a member of the new layer and
            // that is is not a member of the old layer
            Assert.IsTrue(testDictionary.ContainsLayer("NewLayer"));
            Assert.IsTrue(testDictionary.GetLayer("NewLayer").ContainsEntity(testLine1.Handle));
            Assert.IsFalse(testDictionary.GetLayer("TestLayer").ContainsEntity(testLine1.Handle));

            // Now let us move it back
            testLine1.LayerName = "TestLayer";

            // Assert that the layer was changed back to TestLayer
            // Assert that it is no longer a member of NewLayer
            // Assert that the count of Newlayer is 0
            Assert.IsTrue(testDictionary.GetLayer("TestLayer").ContainsEntity(testLine1.Handle));
            Assert.IsFalse(testDictionary.GetLayer("NewLayer").ContainsEntity(testLine1.Handle));
            Assert.IsTrue(testDictionary.GetLayer("NewLayer").Count == 0);
        }
    }
}