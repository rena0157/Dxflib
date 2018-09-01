// Dxflib
// GeoArc.cs
// 
// ============================================================
// 
// Created: 2018-08-05
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System;
using Dxflib.LinAlg;

namespace Dxflib.Geometry
{
    /// <inheritdoc />
    /// <summary>
    ///     The GeoArc Class, a class that defines an arc. Note that
    ///     an arc can be defined in more than one way. If this GeoArc is a
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
    /// </summary>
    public class GeoArc : GeoBase
    {
        #region PrivateFields

        private double _angle;
        private Vertex _arcMiddleVertex;
        private double _bulge;
        private Vertex _centerVertex;
        private double _endAngle;
        private double _radius;
        private double _startAngle;
        private Vertex _vertex0;
        private Vertex _vertex1;

        #endregion

        #region Constructors

        /// <summary>
        ///     GeoArc Constructor: Vertex, Vertex and Bulge (VVB)
        /// </summary>
        /// <param name="vertex0">First Vertex</param>
        /// <param name="vertex1">Second Vertex</param>
        /// <param name="bulge">The Bulge</param>
        public GeoArc(Vertex vertex0, Vertex vertex1, double bulge)
        {
            // Set type
            GeometryEntityType = GeometryEntityTypes.GeoArc;

            // Set Variables
            _vertex0 = vertex0;
            _vertex0.GeometryChanged += OnVertexPropertyChanged;
            _vertex1 = vertex1;
            _vertex1.GeometryChanged += OnVertexPropertyChanged;
            _bulge = bulge;

            // Calculate Geometry
            UpdateGeometry(this, new GeometryChangedHandlerArgs("BuildVVB"));
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

            // Set Variables from Constructor 
            _centerVertex = centerVertex;
            _centerVertex.GeometryChanged += OnVertexPropertyChanged;
            _startAngle = startAngle;
            _endAngle = endAngle;
            _radius = radius;

            // Calculate Geometry
            UpdateGeometry(this, new GeometryChangedHandlerArgs("BuildCAAR"));
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
            _vertex0 = vertex0;
            _arcMiddleVertex = middleVertex;
            _vertex1 = vertex1;

            UpdateGeometry(this, new GeometryChangedHandlerArgs("Build3V"));
        }

        #endregion

        #region Properties

        /// <summary>
        ///     The Length of the GeoArc
        /// </summary>
        public double Length { get; private set; }

        /// <summary>
        ///     The Total Angle of the Arc
        /// </summary>
        /// <remarks>
        ///     On Changing this value <see cref="Vertex1" />
        ///     will be moved to a new location that corresponds with the
        ///     new total angle.
        /// </remarks>
        public double Angle
        {
            get => _angle;
            set
            {
                _angle = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("Angle"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("Angle"));
            }
        }

        /// <summary>
        ///     The Radius of the Arc
        /// </summary>
        /// <remarks>
        ///     On Changing this property <see cref="Vertex0" />
        ///     and <see cref="Vertex1" /> will be recalculated
        ///     using <see cref="CalcPointOnArc" /> with the new Radius Given
        ///     and the same center point.
        ///     Also, When this property is change the
        ///     <see cref="GeoBase.GeometryChanged" />
        ///     Event will be thrown.
        /// </remarks>
        public double Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("Radius"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("Radius"));
            }
        }

        /// <summary>
        ///     The Area of the GeoArc
        /// </summary>
        public double Area { get; private set; }

        /// <summary>
        ///     The Starting Angle of the arc (Radians)
        /// </summary>
        /// <remarks>
        ///     On Changing this property <see cref="Vertex0" />
        ///     will be updated using <see cref="CalcPointOnArc" /> with
        ///     the new angle given.
        ///     Also, when this property is changed the
        ///     <see cref="GeoBase.GeometryChanged" /> event will fire.
        /// </remarks>
        public double StartAngle
        {
            get => _startAngle;
            set
            {
                _startAngle = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("StartAngle"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("StartAngle"));
            }
        }

        /// <summary>
        ///     The Ending Angle of the arc (Radians)
        /// </summary>
        /// <remarks>
        ///     On Changing this property <see cref="Vertex1" />
        ///     will change and the <see cref="GeoBase.GeometryChanged" /> with the
        ///     given new angle
        ///     event will fire.
        /// </remarks>
        public double EndAngle
        {
            get => _endAngle;
            set
            {
                _endAngle = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("EndAngle"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("EndAngle"));
            }
        }

        /// <summary>
        ///     The First Vertex
        /// </summary>
        /// <remarks>
        ///     On changing the property the GeoArc will update and the
        ///     <see cref="GeoBase.GeometryChanged" /> event will fire.
        /// </remarks>
        public Vertex Vertex0
        {
            get => _vertex0;
            set
            {
                _vertex0 = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("Vertex0"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("Build3V"));
            }
        }

        /// <summary>
        ///     The Second Vertex
        /// </summary>
        /// <remarks>
        ///     On changing this property the GeoArc will update and the
        ///     <see cref="GeoBase.GeometryChanged" /> event will fire.
        /// </remarks>
        public Vertex Vertex1
        {
            get => _vertex1;
            set
            {
                _vertex1 = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("Vertex1"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("Build3V"));
            }
        }

        /// <summary>
        ///     The Circular center point of the arc
        /// </summary>
        /// <remarks>
        ///     On changing this property the GeoArc will update and the
        ///     <see cref="GeoBase.GeometryChanged" /> event will fire.
        /// </remarks>
        public Vertex CenterVertex
        {
            get => _centerVertex;
            set
            {
                _centerVertex = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("CenterVertex"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("BuildCAAR"));
            }
        }

        /// <summary>
        ///     The middle point between the Starting Vertex (<see cref="Vertex0" />)
        ///     and the Ending Vertex (<see cref="Vertex1" />) that lies on the path
        ///     of the arc.
        /// </summary>
        /// <remarks>
        ///     On changing this property the GeoArc will update and the
        ///     <see cref="GeoBase.GeometryChanged" /> event will fire.
        /// </remarks>
        public Vertex MiddleVertex
        {
            get => _arcMiddleVertex;
            set
            {
                _arcMiddleVertex = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("MiddleVertex"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("MiddleVertex"));
            }
        }

        /// <summary>
        ///     The bulge value for this object (Is similar to the curvature of an arc)
        /// </summary>
        /// <remarks>
        ///     On changing this property the GeoArc will update and the
        ///     <see cref="GeoBase.GeometryChanged" /> event will fire.
        /// </remarks>
        public double BulgeValue
        {
            get => _bulge;
            set
            {
                _bulge = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("BulgeValue"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("BuildVVB"));
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
        public static Vertex CalcCenterVertex(Vertex vertex0, Vertex vertex1, double bulge)
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
        public static Vertex CalcCenterVertex(Vertex vertex0, Vertex vertexOnArc, Vertex vertex1)
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
        public static Vertex CalcPointOnArc(Vertex centerVertex, double angle, double radius)
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
        ///     Method that calculates the middle point on the arc
        /// </summary>
        /// <returns>A Vertex that is the middle point on the arc</returns>
        private Vertex GetMiddlePoint()
        {
            var minAngle = Math.Min(StartAngle, EndAngle);
            var maxAngle = Math.Max(StartAngle, EndAngle);
            var middlePointAngle = ( maxAngle - minAngle ) / 2 + minAngle;
            return CalcPointOnArc(CenterVertex, middlePointAngle, Radius);
        }

        private void OnVertexPropertyChanged(object sender, GeometryChangedHandlerArgs args)
        {
            if ( sender == _vertex0 )
                UpdateGeometry(this, new GeometryChangedHandlerArgs("Vertex0"));
            else if ( sender == _vertex1 )
                UpdateGeometry(this, new GeometryChangedHandlerArgs("Vertex1"));
            else if ( sender == _centerVertex )
                UpdateGeometry(this, new GeometryChangedHandlerArgs("CenterVertex"));
        }

        /// <inheritdoc />
        /// <summary>
        ///     Update the Geometry of the GeoArc
        /// </summary>
        /// <param name="sender">Usually this</param>
        /// <param name="args">Arguments</param>
        // ReSharper disable once InconsistentNaming
        protected sealed override void UpdateGeometry(
            object sender, GeometryChangedHandlerArgs args)
        {
            switch ( args.Name )
            {
                case "BuildVVB":
                {
                    _angle = Bulge.Angle(_bulge);
                    _radius = Bulge.Radius(_vertex0, _vertex1, Angle);

                    _centerVertex = CalcCenterVertex(_vertex0, _vertex1, _bulge);
                    _centerVertex.GeometryChanged += OnVertexPropertyChanged;

                    _startAngle = Vector.AngleBetweenVectors(
                        new Vector(_centerVertex, _vertex0), UnitVectors.XUnitVector);
                    _endAngle = Vector.AngleBetweenVectors(
                        new Vector(_centerVertex, _vertex1), UnitVectors.XUnitVector);

                    // The Middle Vertex
                    _arcMiddleVertex = GetMiddlePoint();

                    // Get Only Properties
                    Length = GeoMath.Distance(_vertex0, _vertex1, _bulge);
                    Area = GeoMath.ChordArea(this);
                }
                    break;

                case "BuildCAAR":
                {
                    _angle = _endAngle - _startAngle;
                    _bulge = Math.Tan(_angle / 4);

                    _vertex0 = CalcPointOnArc(_centerVertex, _startAngle, _radius);
                    _vertex0.GeometryChanged += OnVertexPropertyChanged;

                    _vertex1 = CalcPointOnArc(_centerVertex, _endAngle, _radius);
                    _vertex1.GeometryChanged += OnVertexPropertyChanged;

                    // Middle Vertex
                    _arcMiddleVertex = GetMiddlePoint();

                    // Get only properties
                    Length = GeoMath.Distance(_vertex0, _vertex1, _bulge);
                    Area = GeoMath.ChordArea(this);
                }
                    break;

                case "Build3V":
                {
                    _centerVertex = CalcCenterVertex(_vertex0, _arcMiddleVertex, _vertex1);

                    _startAngle = Vector.AngleBetweenVectors(
                        new Vector(_centerVertex, _vertex0).ToUnitVector(),
                        UnitVectors.XUnitVector);

                    _endAngle = Vector.AngleBetweenVectors(
                        new Vector(_centerVertex, _vertex1).ToUnitVector(),
                        UnitVectors.XUnitVector);

                    _angle = _endAngle - _startAngle;
                    _bulge = Bulge.CalcBulge(_angle);
                    _radius = new GeoLine(_centerVertex, _arcMiddleVertex).Length;

                    Length = GeoMath.Distance(_vertex0, _vertex1, _bulge);
                    Area = GeoMath.ChordArea(this);
                }
                    break;

                case "Angle":
                {
                    // Update properties
                    _endAngle = _startAngle + _angle;
                    _bulge = Bulge.CalcBulge(_angle);

                    // Move the end vertex over by the change in angle
                    _vertex1 = CalcPointOnArc(_centerVertex, _endAngle, _radius);
                    _vertex1.GeometryChanged += OnVertexPropertyChanged;

                    // middle vertex
                    _arcMiddleVertex = GetMiddlePoint();

                    Length = GeoMath.Distance(_vertex0, _vertex1, _bulge);
                    Area = GeoMath.ChordArea(this);
                }
                    break;
                case "Radius":
                {
                    _bulge = Math.Tan(_angle / 4);

                    // Recalculate the starting vertex
                    _vertex0 = CalcPointOnArc(_centerVertex, _startAngle, _radius);
                    _vertex0.GeometryChanged += OnVertexPropertyChanged;

                    // Recalculate the ending vertex
                    _vertex1 = CalcPointOnArc(_centerVertex, _endAngle, _radius);
                    _vertex1.GeometryChanged += OnVertexPropertyChanged;

                    // middle vertex
                    _arcMiddleVertex = GetMiddlePoint();

                    // Get only properties
                    Length = GeoMath.Distance(_vertex0, _vertex1, _bulge);
                    Area = GeoMath.ChordArea(this);
                }
                    break;
                case "StartAngle":
                {
                    _angle = _endAngle - _startAngle;

                    // Move the starting vertex over
                    _vertex0 = CalcPointOnArc(_centerVertex, _startAngle, _radius);
                    _vertex0.GeometryChanged += OnVertexPropertyChanged;

                    _arcMiddleVertex = GetMiddlePoint();

                    // Get only properties
                    Length = GeoMath.Distance(_vertex0, _vertex1, _bulge);
                    Area = GeoMath.ChordArea(this);
                }
                    break;
                case "EndAngle":
                {
                    _angle = _endAngle - _startAngle;

                    _vertex1 = CalcPointOnArc(_centerVertex, _endAngle, _radius);
                    _vertex1.GeometryChanged += OnVertexPropertyChanged;

                    _arcMiddleVertex = GetMiddlePoint();

                    // Get only properties
                    Length = GeoMath.Distance(_vertex0, _vertex1, _bulge);
                    Area = GeoMath.ChordArea(this);
                }
                    break;
                // ReSharper disable once RedundantEmptySwitchSection
                default:
                    break;
            }
        }

        #endregion
    }
}