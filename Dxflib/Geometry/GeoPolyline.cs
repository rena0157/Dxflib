// Dxflib
// GeoPolyline.cs
// 
// ============================================================
// 
// Created: 2018-08-07
// Last Updated: 2018-08-07-11:36 AM
// By: Adam Renaud
// 
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Dxflib.Entities;

namespace Dxflib.Geometry
{
    /// <inheritdoc />
    /// <summary>
    /// The GeoPolyline Class, which is a container of the GeoMetricEntityBase.
    /// This Object contains both GeoArcs and GeoLines
    /// </summary>
    public class GeoPolyline : GeometricEntityBase
    {
        /// <summary>
        /// The Main GeoPolyLine Constructor
        /// </summary>
        /// <param name="xValues">X Coordinates</param>
        /// <param name="yValues">Y Coordinates</param>
        /// <param name="bulgeList">A List of Bulges</param>
        /// <param name="polylineFlag">The isClosed Property of a polyline</param>
        public GeoPolyline(List<double> xValues,
            List<double> yValues, List<double> bulgeList, bool polylineFlag)
        {
            // Throw Exception if the number of verticies does not match the 
            if (xValues.Count != yValues.Count)
                throw new EntityException("There must be a matching number of verticies");

            // Initalize and preallocate the vertex list
            Vertices = new List<Vertex>(xValues.Capacity);

            // Initalizing a preallicating the section list
            SecionList = polylineFlag
                ? new List<GeometricEntityBase>(Vertices.Capacity)
                : new List<GeometricEntityBase>(Vertices.Capacity - 1);

            // Place x and y into the verticies list
            Vertices.AddRange(xValues.Select((t, vertexIndex)
                => new Vertex(t, yValues[vertexIndex])));

            // Iterate through all of the verticies and build either GeoArcs or GeoLines
            for (var vertexIndex = 0; vertexIndex < Vertices.Count; ++vertexIndex)
            {
                var currentVertex = Vertices[vertexIndex];
                var nextVertex
                    = vertexIndex == Vertices.Count - 1 ? Vertices[0] : Vertices[vertexIndex + 1];

                if (Math.Abs(bulgeList[vertexIndex] - Bulge.BulgeNull) > GeoMath.Tolerance)
                {
                    var geoArc = new GeoArc(currentVertex, nextVertex, bulgeList[vertexIndex]);
                    geoArc.GeometryChanged += UpdateGeometry;
                    SecionList.Add(geoArc);
                }
                else
                {
                    var geoLine = new GeoLine(currentVertex, nextVertex);
                    geoLine.GeometryChanged += UpdateGeometry;
                    SecionList.Add(new GeoLine(currentVertex, nextVertex));
                }


                if (!polylineFlag)
                    SecionList.RemoveAt(SecionList.Count - 1);
            }

            UpdateGeometry(this, new GeometryChangedHandlerArgs(0));
        }

        /// <summary>
        /// The Sections
        /// </summary>
        private List<GeometricEntityBase> SecionList { get; }

        /// <summary>
        /// The Vertices
        /// </summary>
        private List<Vertex> Vertices { get; }

        /// <summary>
        /// The Total Length of the Polyline
        /// </summary>
        public double Length { get; private set; }

        /// <summary>
        /// The Total Area of the polyline
        /// </summary>
        public double Area { get; private set; }


        /// <summary>
        /// Override of the Update Geometry method from the GeometricEntityBase class
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="args">The Arguments</param>
        protected sealed override void UpdateGeometry(object sender, GeometryChangedHandlerArgs args)
        {
            Length = CalcLength();
            Area = CalcArea();
        }

        /// <summary>
        /// The CalcLength function that calculates the total length of the polyline
        /// </summary>
        /// <returns>The Total length of all sections</returns>
        protected sealed override double CalcLength()
        {
            var sumLength = 0.0;
            foreach (var entity in SecionList)
                switch (entity.GeometryEntityType)
                {
                    case GeometryEntityTypes.Vertex:
                        break;
                    case GeometryEntityTypes.GeoLine:
                        sumLength += ((GeoLine) entity).Length;
                        break;
                    case GeometryEntityTypes.GeoArc:
                        sumLength += ((GeoArc) entity).Length;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            return sumLength;
        }

        private double CalcArea()
        {
            var sumArea = 0.0;
            foreach ( var entity in SecionList )
            {
                switch ( entity.GeometryEntityType )
                {
                    case GeometryEntityTypes.Vertex:
                        break;
                    case GeometryEntityTypes.GeoLine:
                        sumArea += ( (GeoLine) entity ).Area;
                        break;
                    case GeometryEntityTypes.GeoArc:
                        sumArea += ( (GeoArc) entity ).Area;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return sumArea;
        }
    }
}