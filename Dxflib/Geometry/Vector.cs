// Dxflib
// Vector.cs
// 
// ============================================================
// 
// Created: 2018-08-08
// Last Updated: 2018-08-08-7:02 PM
// By: Adam Renaud
// 
// ============================================================

using System;

namespace Dxflib.Geometry
{
    /// <summary>
    ///     The Vector Class: Defines vector mathematics and functionality
    ///     for the dxf library
    /// </summary>
    public class Vector
    {
        private Vertex _vertex0;
        private Vertex _vertex1;

        /// <summary>
        ///     Default Constructor
        /// </summary>
        public Vector()
        {
            _vertex0 = new Vertex(0, 0);
            _vertex1 = new Vertex(1, 1, 1);
            UpdateGeometry(this, new GeometryChangedHandlerArgs("Build"));
            SubscribeToEvents();
        }

        /// <summary>
        ///     PositionVector Constructor
        /// </summary>
        /// <param name="startingVertex">The Starting Vertex for the position vector</param>
        /// <param name="endingVertex">The Ending Vertex for the position vector</param>
        public Vector(Vertex startingVertex, Vertex endingVertex)
        {
            _vertex0 = startingVertex;
            _vertex1 = endingVertex;
            UpdateGeometry(this, new GeometryChangedHandlerArgs("Build"));
            SubscribeToEvents();
        }

        /// <summary>
        ///     UnitVector Constructor
        /// </summary>
        /// <param name="xComponent">The x component of a unit vector</param>
        /// <param name="yComponent">The y component of a unit vector</param>
        /// <param name="zComponent">The z component of a unit vector</param>
        public Vector(double xComponent, double yComponent, double zComponent = 0)
        {
            _vertex0 = new Vertex(0, 0);
            _vertex1 = new Vertex(xComponent, yComponent, zComponent);
            UpdateGeometry(this, new GeometryChangedHandlerArgs("Build"));
            SubscribeToEvents();
        }

        /// <summary>
        ///     The vector's x component
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        ///     The vector's y component
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        ///     The vector's z component
        /// </summary>
        public double Z { get; private set; }

        /// <summary>
        ///     The vector's lenght or magnitude
        /// </summary>
        public double Length { get; private set; }

        /// <summary>
        ///     The Head of the vector or the ending vector (Point of Application)
        /// </summary>
        public Vertex HeadVertex
        {
            get => _vertex1;
            set
            {
                _vertex1 = value;
                OnGeometryChanged(this, EventArgs.Empty);
                UpdateGeometry(this, new GeometryChangedHandlerArgs(1));
            }
        }

        /// <summary>
        ///     The Tail of the vector: where the vector originates
        /// </summary>
        public Vertex TailVertex
        {
            get => _vertex0;
            set
            {
                _vertex0 = value;
                OnGeometryChanged(this, EventArgs.Empty);
                UpdateGeometry(this, new GeometryChangedHandlerArgs(0));
            }
        }

        /// <summary>
        ///     The event when ever one of the vertexes change or if one of the components change
        /// </summary>
        public event EventHandler GeometryChanged;

        /// <summary>
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="args">Some arguments</param>
        private void UpdateGeometry(object sender, GeometryChangedHandlerArgs args)
        {
            X = _vertex1.X - _vertex0.X;
            Y = _vertex1.Y - _vertex0.Y;
            Z = _vertex1.Z - _vertex0.Z;
            Length = GeoMath.Distance(_vertex0, _vertex1);
        }

        /// <summary>
        ///     Subscribing to vertex events
        /// </summary>
        private void SubscribeToEvents()
        {
            _vertex0.GeometryChanged += UpdateGeometry;
            _vertex1.GeometryChanged += UpdateGeometry;
        }

        /// <summary>
        ///     On Geometry Changed invokation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnGeometryChanged(object sender, EventArgs args) { GeometryChanged?.Invoke(sender, args); }

        /// <summary>
        ///     Creates a new vector that is the unit vector of this vector
        /// </summary>
        /// <returns>A new Vector object</returns>
        public Vector ToUnitVector()
        {
            return new Vector(X / Length,
                Y / Length,
                Z / Length);
        }

        /// <summary>
        ///     The Mathematical Dot Product of two vectors
        /// </summary>
        /// <param name="other">The other vector in the dot product equation</param>
        /// <returns>A double value that is the dot product of other and this vector</returns>
        public double DotProduct(Vector other) { return other.X * X + other.Y * Y + other.Z * Z; }

        /// <summary>
        ///     The Cross product of this vector and the other vector (ThisVector x OtherVector)
        /// </summary>
        /// <param name="other">The other vector that is in the cross function</param>
        /// <returns>A new vector that is the resultant of the cross product</returns>
        public Vector CrossProduct(Vector other)
        {
            return new Vector(
                Y * other.Z - Z * other.Y,
                -( X * other.Z - other.X * Z ),
                X * other.Y - other.X * Y);
        }
    }
}