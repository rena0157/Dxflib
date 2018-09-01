// Dxflib
// GeoArc.cs
// 
// ============================================================
// 
// Created: 2018-08-05
// Last Updated: 2018-09-01-6:42 PM
// By: Adam Renaud
// 
// ============================================================

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Dxflib.LinAlg;

namespace Dxflib.Geometry
{
    /// <inheritdoc cref="GeoBase" />
    /// <inheritdoc cref="IGeoLinear"/>
    /// <summary>
    ///     The GeoArc Class: Defines a Geometric Circular Arc.
    /// </summary>
    /// <remarks>
    ///     Note that an arc can be defined in more than one way. If this GeoArc is a
    ///     part of an LwPolyLine then is defined using a bulge. If it is
    ///     alone then it is defined using a radius.
    ///     Note that there are 3 distinct ways to construct an Arc
    ///     1. The First way is the Vertex, Vertex and Bulge (VVB) Method
    ///     This Method uses two vertices that are at endpoints of the arc and
    ///     a numerical values called the bulge.
    ///     2. The Second Method is the Center Point, Starting Angle, Ending Angle and Radius
    ///     (CAAR) Method. The CAAR method uses a point that will be the center point of the arc
    ///     the starting angle of the arc and the ending angle of the arc as well as the radius
    ///     of the arc. This method is mostly used if a user would like to create an arc. Also,
    ///     this method is used in the AutoCAD ARC Entity.
    ///     3. The Third Method is the Vertex, Vertex, Vertex (V3) Method. The V3 method uses
    ///     3 vertices that the arc must pass though. The first and second vertex are the starting
    ///     and ending vertices. The middle vertex can be any vertex that is on the arc. During
    ///     the moving of one vertex the center arc length is usually chosen as the middle vertex.
    ///
    ///     All of the set Properties in this class will cause the geometry of other properties
    ///     to be updated when they are set.
    /// </remarks>
    public class GeoArc : GeoBase, IGeoLinear
    {
        #region PrivateFields

        private Vertex _vertex0;         // The Starting Vertex
        private Vertex _vertex1;         // The Ending Vertex
        private Vertex _arcMiddleVertex; // The Vertex At the mid point of the Arc
        private Vertex _centerVertex;    // The Vertex at the Center of the Arc
        private double _bulge;           // The Bulge of the Arc
        private double _startAngle;      // The starting angle of the arc (Vertex0)
        private double _endAngle;        // The ending angle of the arc (Vertex1)
        private double _radius;          // The radius of the arc

        #endregion

        #region Constructors

        /// <summary>
        ///     GeoArc from Starting Vertex (<paramref name="vertex0"/>)
        ///     ending Vertex (<paramref name="vertex1"/>) and a bulge
        /// </summary>
        /// <param name="vertex0">First Vertex</param>
        /// <param name="vertex1">Second Vertex</param>
        /// <param name="bulge">The Bulge</param>
        public GeoArc(Vertex vertex0, Vertex vertex1, double bulge)
        {
            // Set type
            GeometryEntityType = GeometryEntityTypes.GeoArc;
            // Set Variables
            // Vertex0
            _vertex0 = vertex0;
            Vertex0.PropertyChanged += Vertex0OnPropertyChanged;
            // Vertex1
            _vertex1 = vertex1;
            Vertex1.PropertyChanged += Vertex1OnPropertyChanged;
            // Bulge
            _bulge = bulge;
            // Angle
            Angle = Bulge.Angle(BulgeValue);
            // Radius
            _radius = Bulge.Radius(Vertex0, Vertex1, Angle);
            // Center Vertex
            _centerVertex = CalcCenterVertex(Vertex0, Vertex1, BulgeValue);
            CenterVertex.PropertyChanged += CenterVertexOnPropertyChanged;
            // Start Angle
            _startAngle = Vector.AngleBetweenVectors(
                new Vector(CenterVertex, Vertex0), UnitVectors.XUnitVector
            );
            // End Angle
            _endAngle = Vector.AngleBetweenVectors(
                new Vector(CenterVertex, Vertex1), UnitVectors.XUnitVector
            );
            // Middle Vertex
            _arcMiddleVertex = CalcMiddlePoint();
            MiddleVertex.PropertyChanged += MiddleVertexOnPropertyChanged;

            // Calculate Geometry
            Length = CalcLength();
            Area = CalcArea();
        }


        /// <summary>
        ///     GeoArc Constructor: Center Vertex, Starting Angle, Ending Angle and Radius (CAAR)
        /// </summary>
        /// <param name="centerVertex">The Center Vertex of the GeoArc</param>
        /// <param name="startAngle">The Starting Angle (Radians)</param>
        /// <param name="endAngle">The Ending Angle (Radians)</param>
        /// <param name="radius">The Radius of the Arc</param>
        public GeoArc(Vertex centerVertex, double startAngle, double endAngle, double radius)
        {
            // Set Type
            GeometryEntityType = GeometryEntityTypes.GeoArc;
            // Center Vertex
            _centerVertex = centerVertex;
            CenterVertex.PropertyChanged += CenterVertexOnPropertyChanged;

            // Start Angle
            _startAngle = startAngle;

            // End Angle
            _endAngle = endAngle;

            // Radius
            _radius = radius;

            // Angle
            Angle = EndAngle - StartAngle;

            // Bulge
            _bulge = Bulge.CalcBulge(Angle);

            // Vertex0
            _vertex0 = CalcPointOnArc(CenterVertex, StartAngle, Radius);
            Vertex0.PropertyChanged += Vertex0OnPropertyChanged;

            // Vertex1
            _vertex1 = CalcPointOnArc(CenterVertex, EndAngle, Radius);
            Vertex1.PropertyChanged += Vertex1OnPropertyChanged;

            // Middle Vertex
            _arcMiddleVertex = CalcMiddlePoint();
            MiddleVertex.PropertyChanged += MiddleVertexOnPropertyChanged;

            Length = CalcLength();
            Area = CalcArea();
        }

        /// <summary>
        ///     Geo Arc Constructor: Vertex, Vertex, Vertex (V3)
        ///     The V3 Constructor takes three vertices that the arc must pass through and
        ///     calculates all of the other properties of the arc
        /// </summary>
        /// <param name="vertex0">The Starting Vertex</param>
        /// <param name="middleVertex">The Middle Vertex</param>
        /// <param name="vertex1">The Ending Vertex</param>
        public GeoArc(Vertex vertex0, Vertex middleVertex, Vertex vertex1)
        {
            // Vertex 0
            _vertex0 = vertex0;
            Vertex0.PropertyChanged += Vertex0OnPropertyChanged;

            // Middle Vertex
            _arcMiddleVertex = middleVertex;
            MiddleVertex.PropertyChanged += MiddleVertexOnPropertyChanged;

            // Vertex 1
            _vertex1 = vertex1;
            Vertex1.PropertyChanged += Vertex1OnPropertyChanged;

            // Center Vertex
            _centerVertex = CalcCenterVertex(Vertex0, MiddleVertex, Vertex1);
            CenterVertex.PropertyChanged += CenterVertexOnPropertyChanged;

            // Starting Angle
            _startAngle = Vector.AngleBetweenVectors(
                new Vector(CenterVertex, vertex0), UnitVectors.XUnitVector
            );

            // Ending Angle
            _endAngle = Vector.AngleBetweenVectors(
                new Vector(CenterVertex, Vertex1), UnitVectors.XUnitVector
            );

            // Total Angle
            Angle = EndAngle - StartAngle;

            // Bulge
            _bulge = Bulge.CalcBulge(Angle);

            // Radius
            _radius = GeoMath.Distance(CenterVertex, vertex0);

            // Geometric Properties
            Length = CalcLength();
            Area = CalcArea();
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        /// <summary>
        ///     Arc Length of the GeoArc
        /// </summary>
        public double Length { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     Area of the GeoArc
        /// </summary>
        /// <remarks>
        ///     <see cref="Area"/> is defined as the geometric area
        ///     between the arc and the segment that defines its chord.
        ///     <see cref="GeoMath.ChordArea"/> is the function that is used
        ///     to calculate the area of this class
        /// </remarks>
        public double Area { get; private set; }

        /// <summary>
        ///     Total Angle of the Arc (Radians)
        /// </summary>
        /// <remarks>
        ///     Total angle = <see cref="EndAngle"/> - <see cref="StartAngle"/>
        /// </remarks>
        public double Angle { get; private set; }

        /// <summary>
        ///     The Radius of the Arc
        /// </summary>
        public double Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                RadiusOnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The Starting Angle of the arc (Radians)
        /// </summary>
        /// <remarks>
        ///     Starting Angle is defined as the angle between two vectors:
        ///     1. Vector that starts at <see cref="CenterVertex"/> and ends at <see cref="Vertex0"/>
        ///     2. The <see cref="UnitVectors.XUnitVector"/>.
        ///     This Angle is measured in radians
        /// </remarks>
        public double StartAngle
        {
            get => _startAngle;
            set
            {
                _startAngle = value;
                StartAngleOnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The Ending Angle of the arc (Radians)
        /// </summary>
        /// <remarks>
        ///     Starting Angle is defined as the angle between two vectors:
        ///     1. Vector that starts at <see cref="CenterVertex"/> and ends at <see cref="Vertex1"/>
        ///     2. The <see cref="UnitVectors.XUnitVector"/>.
        ///     This Angle is measured in radians
        /// </remarks>
        public double EndAngle
        {
            get => _endAngle;
            set
            {
                _endAngle = value;
                EndAngleOnPropertyChanged();
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The First Vertex
        /// </summary>
        public Vertex Vertex0
        {
            get => _vertex0;
            set
            {
                _vertex0 = value;
                Vertex0OnPropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The Second Vertex
        /// </summary>
        public Vertex Vertex1
        {
            get => _vertex1;
            set
            {
                _vertex1 = value;
                Vertex1OnPropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The Circular center point of the arc
        /// </summary>
        public Vertex CenterVertex
        {
            get => _centerVertex;
            set
            {
                _centerVertex = value;
                CenterVertexOnPropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The middle point between the Starting Vertex (<see cref="Vertex0" />)
        ///     and the Ending Vertex (<see cref="Vertex1" />) that lies on the path
        ///     of the arc.
        /// </summary>
        public Vertex MiddleVertex
        {
            get => _arcMiddleVertex;
            set
            {
                _arcMiddleVertex = value;
                MiddleVertexOnPropertyChanged(this, new PropertyChangedEventArgs(string.Empty));
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     The bulge value for this object (Is similar to the curvature of an arc)
        /// </summary>
        public double BulgeValue
        {
            get => _bulge;
            set
            {
                _bulge = value;
                BulgeValueOnPropertyChanged();
                OnPropertyChanged();
            }
        }

        #endregion

        #region StaticMethods

        /// <summary>
        ///     Calculates the Center Vertex
        /// </summary>
        /// <code>
        ///      // Create the vertices and the bulge
        ///      var vertex1 = new Vertex(2, 2);
        ///      var vertex0 = new Vertex(0, 2);
        ///      const double bulgeValue = -0.5;
        /// 
        ///      // Build the Vertex
        ///      var testVertex = CalcCenterVertex(vertex0, vertex1, bulgeValue);
        ///      // Should create a Vertex == (1, 1.25)
        ///  </code>
        /// <returns>A <see cref="Vertex" /> object</returns>
        private static Vertex CalcCenterVertex(Vertex vertex0, Vertex vertex1, double bulge)
        {
            // Vector between V0 and V1
            var vector0 = new Vector(vertex0, vertex1);

            // Unit vector of vector0
            var unitVector = vector0.ToUnitVector();

            // if the bulge is less than 0 rotate CW 90 degrees
            if ( bulge < 0 )
                unitVector.Rotate(-Math.PI / 2);
            // Rotate CCW 90 degrees
            else
                unitVector.Rotate(Math.PI / 2);

            // Scale the vector
            unitVector.Scale(Bulge.Radius(vertex0, vertex1, Bulge.Angle(bulge))
                             * Math.Cos(Bulge.Angle(bulge) / 2));

            // Move the vector into place between v0 and v1
            unitVector.Translate(new Vertex(
                vector0.TailVertex.X + vector0.X / 2,
                vector0.TailVertex.Y + vector0.Y / 2));

            return new Vertex(unitVector.HeadVertex.X, unitVector.HeadVertex.Y);
        }

        /// <summary>
        ///     Calculates the center vertex from 3 other vertices.
        /// </summary>
        /// <param name="vertex0">The Starting Vertex</param>
        /// <param name="vertexOnArc">Any Vertex that the arc will pass through</param>
        /// <param name="vertex1">The Ending Vertex</param>
        /// <returns>The center vertex of the arc</returns>
        private static Vertex CalcCenterVertex(Vertex vertex0, Vertex vertexOnArc, Vertex vertex1)
        {
            var centerVertex = new Vertex(0, 0);

            // Slope between the first vertex and the second
            var m1 = ( vertexOnArc.Y - vertex0.Y ) / ( vertexOnArc.X - vertex0.X );

            // Slope between the second and the third vertex
            var m2 = ( vertex1.Y - vertexOnArc.Y ) / ( vertex1.X - vertexOnArc.X );

            // Vertex that is at the center point on a line that is drawn
            // between vertex0 and vertexOnArc
            var vertexA = new Vertex(
                vertex0.X + ( vertexOnArc.X - vertex0.X ) / 2,
                vertex0.Y + ( vertexOnArc.Y - vertex0.Y ) / 2);

            // Vertex that is at the center point on a line that is drawn
            // between vertexOnArc and vertex1
            var vertexB = new Vertex(
                vertexOnArc.X + ( vertex1.X - vertexOnArc.X ) / 2,
                vertexOnArc.Y + ( vertex1.Y - vertexOnArc.Y ) / 2);

            // Calculating and setting the x coordinate of the center vertex
            centerVertex.X = ( vertexB.Y - vertexA.Y + vertexB.X / m2 - vertexA.X / m1 ) /
                             ( 1 / m2 - 1 / m1 );
            // Calculating and setting the y coordinate of the center vertex
            centerVertex.Y = -1 / m1 * ( centerVertex.X - vertexA.X ) + vertexA.Y;
            // Return the centerVertex
            return centerVertex;
        }

        /// <summary>
        ///     A function that takes the center vertex of a circle or arc and
        ///     can get any vertex that lies on the arc that corresponds with the angle given.
        /// </summary>
        /// <param name="centerVertex">The center vertex of the circle/arc</param>
        /// <param name="angle">The angle at which the vertex is located</param>
        /// <param name="radius">The radius of the circle/arc</param>
        /// <returns>A vertex that lies on the edge of a circle or arc</returns>
        private static Vertex CalcPointOnArc(Vertex centerVertex, double angle, double radius)
        {
            var vector = new Vector(UnitVectors.XUnitVector);
            vector.Rotate(angle);
            vector.Scale(radius);
            return new Vertex(
                vector.HeadVertex.X + centerVertex.X,
                vector.HeadVertex.Y + centerVertex.Y,
                vector.HeadVertex.Z + centerVertex.Z);
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        ///     The property Changed Function
        /// </summary>
        /// <param name="propertyName"></param>
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
        }

        /// <summary>
        ///     Converts this object to a <see cref="Vector" />.
        ///     Where the <see cref="Vector.HeadVertex" /> and
        ///     <see cref="Vector.TailVertex" /> correspond with
        ///     the <see cref="Vertex0" /> and <see cref="Vertex1" />
        ///     of this object respectively.
        /// </summary>
        /// <returns>A new <see cref="Vector" /> Object</returns>
        public Vector ToVector() { return new Vector(_vertex0, _vertex1); }

        /// <summary>
        ///     Calculate the Length of the Arc
        /// </summary>
        /// <returns>Arc length of the GeoArc</returns>
        private double CalcLength() { return GeoMath.Distance(Vertex0, Vertex1, BulgeValue); }

        /// <summary>
        ///     Calculate the Area
        /// </summary>
        /// <returns></returns>
        private double CalcArea() { return GeoMath.ChordArea(this); }

        /// <summary>
        ///     Method that calculates the middle point on the arc
        /// </summary>
        /// <returns>A Vertex that is the middle point on the arc</returns>
        private Vertex CalcMiddlePoint()
        {
            var minAngle = Math.Min(StartAngle, EndAngle);
            var maxAngle = Math.Max(StartAngle, EndAngle);
            var middlePointAngle = ( maxAngle - minAngle ) / 2 + minAngle;
            return CalcPointOnArc(CenterVertex, middlePointAngle, Radius);
        }

        /// <summary>
        /// </summary>
        /// <param name="command"></param>
        protected override void UpdateGeometry(string command = "")
        {
            Length = CalcLength();
            Area = CalcArea();
        }

        #endregion

        #region OnPropertyChangedMethods

        private void RadiusOnPropertyChanged()
        {
            // Vertex0
            _vertex0 = CalcPointOnArc(CenterVertex, StartAngle, Radius);
            Vertex0.PropertyChanged += Vertex0OnPropertyChanged;
            // Vertex1
            _vertex1 = CalcPointOnArc(CenterVertex, EndAngle, Radius);
            Vertex1.PropertyChanged += Vertex1OnPropertyChanged;
            // Middle Vertex
            _arcMiddleVertex = CalcMiddlePoint();
            MiddleVertex.PropertyChanged += MiddleVertexOnPropertyChanged;
            UpdateGeometry();
        }

        private void StartAngleOnPropertyChanged()
        {
            _vertex0 = CalcPointOnArc(CenterVertex, StartAngle, Radius);
            Vertex0.PropertyChanged += Vertex0OnPropertyChanged;
            _arcMiddleVertex = CalcMiddlePoint();
            MiddleVertex.PropertyChanged += MiddleVertexOnPropertyChanged;
            UpdateGeometry();
        }

        private void EndAngleOnPropertyChanged()
        {
            _vertex1 = CalcPointOnArc(CenterVertex, EndAngle, Radius);
            Vertex1.PropertyChanged += Vertex1OnPropertyChanged;
            _arcMiddleVertex = CalcMiddlePoint();
            MiddleVertex.PropertyChanged += MiddleVertexOnPropertyChanged;
            UpdateGeometry();
        }

        private void Vertex0OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _centerVertex = CalcCenterVertex(Vertex0, MiddleVertex, Vertex1);
            CenterVertex.PropertyChanged += CenterVertexOnPropertyChanged;

            _radius = GeoMath.Distance(CenterVertex, Vertex0);
            _startAngle = Vector.AngleBetweenVectors(new Vector(CenterVertex, Vertex0), UnitVectors.XUnitVector);
            Angle = EndAngle - StartAngle;
            _bulge = Bulge.CalcBulge(Angle);
            UpdateGeometry();
        }

        private void Vertex1OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _centerVertex = CalcCenterVertex(Vertex0, MiddleVertex, Vertex1);
            _radius = GeoMath.Distance(CenterVertex, Vertex0);
            _endAngle = Vector.AngleBetweenVectors(new Vector(CenterVertex, Vertex1), UnitVectors.XUnitVector);
            Angle = EndAngle - StartAngle;
            _bulge = Bulge.CalcBulge(Angle);
            UpdateGeometry();
        }

        private void CenterVertexOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _vertex0 = CalcPointOnArc(CenterVertex, StartAngle, Radius);
            Vertex0.PropertyChanged += Vertex0OnPropertyChanged;

            _vertex1 = CalcPointOnArc(CenterVertex, EndAngle, Radius);
            Vertex1.PropertyChanged += Vertex1OnPropertyChanged;

            _arcMiddleVertex = CalcMiddlePoint();
            MiddleVertex.PropertyChanged += MiddleVertexOnPropertyChanged;
        }

        private void MiddleVertexOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _centerVertex = CalcCenterVertex(Vertex0, MiddleVertex, Vertex1);
            CenterVertex.PropertyChanged += CenterVertexOnPropertyChanged;

            _radius = GeoMath.Distance(CenterVertex, Vertex0);
            _startAngle = Vector.AngleBetweenVectors(new Vector(CenterVertex, Vertex0), UnitVectors.XUnitVector);
            _endAngle = Vector.AngleBetweenVectors(new Vector(CenterVertex, Vertex1), UnitVectors.XUnitVector);
            Angle = EndAngle - StartAngle;
            _bulge = Bulge.CalcBulge(Angle);

            UpdateGeometry();
        }

        private void BulgeValueOnPropertyChanged()
        {
            Angle = Bulge.Angle(BulgeValue);
            _radius = Bulge.Radius(Vertex0, Vertex1, Angle);

            _centerVertex = CalcCenterVertex(Vertex0, Vertex1, BulgeValue);
            CenterVertex.PropertyChanged += CenterVertexOnPropertyChanged;

            _startAngle = Vector.AngleBetweenVectors(
                new Vector(CenterVertex, Vertex0), UnitVectors.XUnitVector
            );
            _endAngle = Vector.AngleBetweenVectors(
                new Vector(CenterVertex, Vertex1), UnitVectors.XUnitVector
            );
            _arcMiddleVertex = CalcMiddlePoint();
            MiddleVertex.PropertyChanged += MiddleVertexOnPropertyChanged;

            UpdateGeometry();
        }
        #endregion
    }
}