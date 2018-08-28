// Dxflib
// GeoPolyline.cs
// 
// ============================================================
// 
// Created: 2018-08-07
// Last Updated: 2018-08-27-9:03 PM
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
    ///     The GeoPolyline Class, which is a container of the GeoMetricEntityBase.
    ///     This Object contains both <see cref="GeoArc"/>s and <see cref="GeoLine"/>s.
    ///     This Object is essentially a wrapper over a <see cref="List{T}"/>
    ///     where T is the <see cref="GeometricEntityBase"/> type.
    /// </summary>
    public class GeoPolyline : GeometricEntityBase
    {
        /// <summary>
        ///     The Main GeoPolyLine Constructor
        /// </summary>
        /// <param name="xValues">X Coordinates</param>
        /// <param name="yValues">Y Coordinates</param>
        /// <param name="bulgeList">A List of Bulges</param>
        /// <param name="polylineFlag">The isClosed Property of a polyline</param>
        public GeoPolyline(List<double> xValues,
            List<double> yValues, List<double> bulgeList, bool polylineFlag)
        {
            // Throw Exception if the number of vertices does not match the 
            if ( xValues.Count != yValues.Count )
                throw new EntityException("There must be a matching number of vertices");

            // Initialize and preallocate the vertex list
            Vertices = new List<Vertex>(xValues.Capacity);

            // Initializing a pre-allocating the section list
            SectionList = polylineFlag
                ? new List<GeometricEntityBase>(Vertices.Capacity)
                : new List<GeometricEntityBase>(Vertices.Capacity - 1);

            // Place x and y into the vertices list
            Vertices.AddRange(xValues.Select((t, vertexIndex)
                => new Vertex(t, yValues[vertexIndex])));

            // Iterate through all of the vertices and build either GeoArcs or GeoLines
            for ( var vertexIndex = 0; vertexIndex < Vertices.Count; ++vertexIndex )
            {
                var currentVertex = Vertices[vertexIndex];
                var nextVertex
                    = vertexIndex == Vertices.Count - 1 ? Vertices[0] : Vertices[vertexIndex + 1];

                if ( Math.Abs(bulgeList[vertexIndex] - Bulge.BulgeNull) > GeoMath.Tolerance )
                {
                    var geoArc = new GeoArc(currentVertex, nextVertex, bulgeList[vertexIndex]);
                    geoArc.GeometryChanged += UpdateGeometry;
                    SectionList.Add(geoArc);
                }
                else
                {
                    var geoLine = new GeoLine(currentVertex, nextVertex);
                    geoLine.GeometryChanged += UpdateGeometry;
                    SectionList.Add(new GeoLine(currentVertex, nextVertex));
                }


                if ( !polylineFlag )
                    SectionList.RemoveAt(SectionList.Count - 1);
            }

            UpdateGeometry(this, new GeometryChangedHandlerArgs(0));
        }

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public GeoPolyline()
        {
            SectionList = new List<GeometricEntityBase>();
            Vertices = new List<Vertex>();
            Length = 0;
            Area = 0;
            IsCounterClockWise = true;
        }

        /// <summary>
        ///     The Sections
        /// </summary>
        private List<GeometricEntityBase> SectionList { get; }

        /// <summary>
        ///     The Vertices
        /// </summary>
        private List<Vertex> Vertices { get; }

        /// <summary>
        ///     The Total Length of the Polyline
        /// </summary>
        public double Length { get; private set; }

        /// <summary>
        ///     The Total Area of the polyline
        /// </summary>
        public double Area { get; private set; }

        /// <summary>
        ///     True: The GeoPolyline was drawn counter clockwise
        /// </summary>
        private bool IsCounterClockWise { get; set; }

        /// <summary>
        ///     The Total number of sections in the section list
        /// </summary>
        public int SectionCount => SectionList.Count;

        /// <summary>
        ///     Add a section to the geo polyline
        /// </summary>
        /// <param name="section"></param>
        public void Add(GeometricEntityBase section)
        {
            // if the section is the right type and
            // is connected to the other sections
            switch ( section.GeometryEntityType )
            {
                case GeometryEntityTypes.Vertex:
                    throw new ArgumentException("The Argument Provided should be either a " +
                                                $"{typeof(GeoLine)} or a {typeof(GeoArc)} not " +
                                                $"a {typeof(Vertex)}");
                case GeometryEntityTypes.GeoLine:
                    // Todo: Add Exception Throw if section is out of range
                    SectionList.Add(section);
                    IsCounterClockWise = IsCounterClockWiseCalc();
                    Length += ( (GeoLine) section ).Length;
                    Area += ( (GeoLine) section ).Area;
                    break;
                case GeometryEntityTypes.GeoArc:
                    // Todo: Add Exception Throw if section is out of range
                    SectionList.Add(section);
                    IsCounterClockWise = IsCounterClockWiseCalc();
                    Length += ( (GeoArc) section ).Length;
                    Area += CalcAreaGeoArc((GeoArc) section);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Override of the Update Geometry method from the GeometricEntityBase class
        /// </summary>
        /// <param name="sender">The object sender</param>
        /// <param name="args">The Arguments</param>
        protected sealed override void UpdateGeometry(object sender, GeometryChangedHandlerArgs args)
        {
            Length = CalcLength();
            Area = CalcArea();
            IsCounterClockWise = IsCounterClockWiseCalc();
        }

        /// <inheritdoc />
        /// <summary>
        ///     The CalcLength function that calculates the total length of the polyline
        /// </summary>
        /// <returns>The Total length of all sections</returns>
        protected sealed override double CalcLength()
        {
            var sumLength = 0.0;
            foreach ( var entity in SectionList )
                switch ( entity.GeometryEntityType )
                {
                    case GeometryEntityTypes.Vertex:
                        break;
                    case GeometryEntityTypes.GeoLine:
                        sumLength += ( (GeoLine) entity ).Length;
                        break;
                    case GeometryEntityTypes.GeoArc:
                        sumLength += ( (GeoArc) entity ).Length;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            return sumLength;
        }

        /// <summary>
        ///     Function that Calculates the total area of the GeoPolyline
        /// </summary>
        /// <returns>The Total Area</returns>
        private double CalcArea()
        {
            var sumArea = 0.0;
            foreach ( var entity in SectionList )
                switch ( entity.GeometryEntityType )
                {
                    case GeometryEntityTypes.Vertex:
                        break;
                    case GeometryEntityTypes.GeoLine:
                        sumArea += ( (GeoLine) entity ).Area;
                        break;
                    case GeometryEntityTypes.GeoArc:
                        sumArea += CalcAreaGeoArc((GeoArc) entity);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

            return sumArea;
        }

        /// <summary>
        ///     Function that calculates the total are of an arc including the
        ///     area that is occupied by a trapezoid under the arc
        /// </summary>
        /// <param name="arc">The arc</param>
        /// <returns>The Area of the Arc and Trapezoid</returns>
        private double CalcAreaGeoArc(GeoArc arc)
        {
            var sumArea = 0.0;
            // Determine if the function should subtract the 
            // bulge
            if ( SubtractBulge(arc.BulgeValue) )
                sumArea -= arc.Area;
            else
                sumArea += arc.Area;

            // Determine if the function should subtract the line
            // that is underneath the bulge as well
            GeoLine line;
            if ( IsCounterClockWise )
                line = new GeoLine(arc.Vertex1,
                    arc.Vertex0);
            else
                line = new GeoLine(arc.Vertex0,
                    arc.Vertex1);
            sumArea += line.Area;

            return sumArea;
        }

        /// <summary>
        ///     Private Function that is used to determine
        ///     if a bulge should be subtracted or added to an arc
        /// </summary>
        /// <param name="bulge">The Bulge Value</param>
        /// <remarks>
        ///     Will always return false if the number of sections
        ///     is less than 2.
        ///     How this function works:
        ///     This function uses the cross product of two vectors vec0 and vec1
        ///     to determine the draw direction of the GeoPolyline. Then with the bulge
        ///     the following Possibilities will follow with (XProduct * BulgeValue)
        ///     1. Bulge +ve | XProd.Z -ve: Subtract the Bulge (ie true)
        ///     2. Bulge +ve | XProd.Z +ve: Add the Bulge (ie false)
        ///     3. Bulge -ve | XProd.Z -ve: Add the Bulge (ie false)
        ///     4. Bulge -ve | XProd.Z +ve: Subtract the Bulge (ie true)
        /// </remarks>
        /// <returns>True: The bulge should be subtracted</returns>
        private bool SubtractBulge(double bulge)
        {
            // If there is less than 2 sections return false
            if ( SectionList.Count < 2 )
                return false;

            // A vector that starts at section0 v0 and ends at section0 v1
            var vec0 = SectionList[0].GeometryEntityType == GeometryEntityTypes.GeoArc
                ? ( (GeoArc) SectionList[0] ).ToVector()
                : ( (GeoLine) SectionList[0] ).ToVector();

            // A vector that starts at section1 v0 and ends at section1 v1
            var vec1 = SectionList[1].GeometryEntityType == GeometryEntityTypes.GeoArc
                ? ( (GeoArc) SectionList[1] ).ToVector()
                : ( (GeoLine) SectionList[1] ).ToVector();

            // See possibilities in the XML remarks comments above the function
            return vec0.CrossProduct(vec1).Z * bulge < 0;
        }

        /// <summary>
        ///     Private function that returns true if the GeoPolyline is counter clockwise
        /// </summary>
        /// <remarks>
        ///     Will always return false if the number of sections is less than 2
        /// </remarks>
        /// <returns>True if the GeoPolyline is Counter Clockwise</returns>
        private bool IsCounterClockWiseCalc()
        {
            // If the section has only 1 argument then 
            // it cannot be counter clockwise or not
            if ( SectionList.Count < 2 )
                return false;

            // A vector that starts at section0 v0 and ends at section0 v1
            var vec0 = SectionList[0].GeometryEntityType == GeometryEntityTypes.GeoArc
                ? ( (GeoArc) SectionList[0] ).ToVector()
                : ( (GeoLine) SectionList[0] ).ToVector();

            // A vector that starts at section1 v0 and ends at section1 v1
            var vec1 = SectionList[1].GeometryEntityType == GeometryEntityTypes.GeoArc
                ? ( (GeoArc) SectionList[1] ).ToVector()
                : ( (GeoLine) SectionList[1] ).ToVector();

            // The cross product of the two vectors vec0 X vec1
            // will be positive if the sections were drawn counter clockwise
            // as per the right hand rule
            return vec0.CrossProduct(vec1).Z > 0;
        }
    }
}