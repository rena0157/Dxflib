// Dxflib
// GeoLine.cs
// 
// ============================================================
// 
// Created: 2018-08-04
// Last Updated: 2018-09-01-3:29 PM
// By: Adam Renaud
// 
// ============================================================

using System.ComponentModel;
using Dxflib.LinAlg;

namespace Dxflib.Geometry
{
    /// <inheritdoc cref="GeoBase" />
    /// <inheritdoc cref="IGeoLinear" />
    /// <summary>
    ///     A Geometric line. This line is different than a line that is
    ///     inheriting the entity class. This line should only be used for geometric
    ///     purposes
    /// </summary>
    public class GeoLine : GeoBase, IGeoLinear
    {
        private Vertex _vertex0;
        private Vertex _vertex1;

        /// <summary>
        ///     Main Constructor of the GeoLine
        /// </summary>
        /// <param name="v0">The First Vertex</param>
        /// <param name="v1">The Second Vertex</param>
        public GeoLine(Vertex v0, Vertex v1)
        {
            // Set type
            GeometryEntityType = GeometryEntityTypes.GeoLine;

            // Set Variables
            _vertex0 = v0;
            _vertex1 = v1;

            // Subscribe to events
            Vertex0.PropertyChanged += Vertex0OnPropertyChanged;
            Vertex1.PropertyChanged += Vertex1OnPropertyChanged;

            // Calculate geometry
            Length = CalcLength();
            Area = CalcArea();
        }

        /// <summary>
        ///     The first Vertex. Note that when setting this property a GeometryChanged
        ///     event will be broadcast. Also changing this property will cause an update
        ///     Geometry method to happen.
        /// </summary>
        public Vertex Vertex0
        {
            get => _vertex0;
            set
            {
                _vertex0 = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The same as <see cref="Vertex0" />. Just the second vertex.
        /// </summary>
        public Vertex Vertex1
        {
            get => _vertex1;
            set
            {
                _vertex1 = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        /// <summary>
        ///     The total length of the polyline.
        /// </summary>
        public double Length { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     The Area of the GeoLine
        /// </summary>
        public double Area { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     Convert this Geoline to a Vector
        /// </summary>
        /// <returns>A new Vector</returns>
        public Vector ToVector() { return new Vector(this); }

        private void Vertex1OnPropertyChanged(object sender, PropertyChangedEventArgs e) { UpdateGeometry(string.Empty); }

        private void Vertex0OnPropertyChanged(object sender, PropertyChangedEventArgs e) { UpdateGeometry(string.Empty); }

        /// <summary>
        ///     Update the Geometry After Property Change
        /// </summary>
        protected override void UpdateGeometry(string command)
        {
            Length = CalcLength();
            Area = CalcArea();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Update the Geometry Before the change
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged(string propertyName = null)
        {
            UpdateGeometry(string.Empty);
            base.OnPropertyChanged(propertyName);
        }

        /// <summary>
        ///     CalcLength Function
        /// </summary>
        /// <returns></returns>
        private double CalcLength() { return GeoMath.Distance(Vertex0, Vertex1); }

        /// <summary>
        ///     Calculate the total area underneath this line and the x-axis
        /// </summary>
        /// <returns>The Area of the line and the x axis</returns>
        private double CalcArea() { return GeoMath.TrapzArea(this); }
    }
}