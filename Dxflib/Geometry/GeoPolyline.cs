// Dxflib
// GeoPolyline.cs
// 
// ============================================================
// 
// Created: 2018-08-07
// Last Updated: 2018-08-07-8:03 AM
// By: Adam Renaud
// 
// ============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using Dxflib.Entities;

namespace Dxflib.Geometry
{
    public class GeoPolyline : GeometricEntityBase
    {
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

            Length = CalcLength();
        }

        public List<GeometricEntityBase> SecionList { get; }

        public List<Vertex> Vertices { get; }

        public double Length { get; }

        public double Area { get; }


        protected override void UpdateGeometry(object sender, GeometryChangedHandlerArgs args)
        {
            CalcLength();
        }

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
    }
}