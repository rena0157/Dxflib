// Dxflib
// GeoArc.cs
// 
// ============================================================
// 
// Created: 2018-08-05
// Last Updated: 2018-08-10-8:55 PM
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
    ///
    ///     Note that there are 3 distinct ways to construct an Arc
    ///     1. The First way is the Vertex, Vertex and Bulge (VVB) Method
    ///     This Method uses two vertices that are at endpoints of the arc and
    ///     a numerical values called the bulge.
    ///
    ///     2. The Second Method is the Center Point, Starting Angle, Ending Angle and Radius
    ///     (CAAR) Method. The CAAR method uses a point that will be the center point of the arc
    ///     the starting angle of the arc and the ending angle of the arc as well as the radius
    ///     of the arc. This method is mostly used if a user would like to create an arc. Also,
    ///     this method is used in the AutoCAD ARC Entity.
    ///
    ///     3. The Third Method is the Vertex, Vertex, Vertex (V3) Method. The V3 method uses
    ///     3 vertices that the arc must pass though. The first and second vertex are the starting
    ///     and ending vertices. The middle vertex can be any vertex that is on the arc. During
    ///     the moving of one vertex the center arc length is usually chosen as the middle vertex.
    /// </summary>
    public class GeoArc : GeometricEntityBase
    {
        private double _angle;
        private Vertex _arcMiddleVertex;
        private double _bulge;
        private Vertex _centerVertex;
        private double _endAngle;
        private double _radius;
        private double _startAngle;
        private Vertex _vertex0;
        private Vertex _vertex1;

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
        /// Geo Arc Constructor: Vertex, Vertex, Vertex (V3)
        /// The V3 Constructor takes three vertices that the arc must pass through and
        /// calculates all of the other properties of the arc
        /// </summary>
        /// <param name="vertex0">The Starting Vertex</param>
        /// <param name="middleVertex">The Middle Vertex</param>
        /// <param name="vertex1">The Ending Vertex</param>
        public GeoArc(Vertex vertex0, Vertex middleVertex, Vertex vertex1)
        {
            // Todo: Create a constructor that takes three vertices and creates a GeoArc
        }

        /// <summary>
        ///     The Length of the GeoArc
        /// </summary>
        public double Length { get; private set; }

        /// <summary>
        ///     The Total Angle of the Arc
        /// </summary>
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
        ///     The Starting Angle of the arc
        /// </summary>
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
        ///     The Ending Angle of the arc
        /// </summary>
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
        public Vertex Vertex0
        {
            get => _vertex0;
            set
            {
                _vertex0 = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("Vertex0"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("Vertex0"));
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
                OnGeometryChanged(new GeometryChangedHandlerArgs("Vertex1"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("Vertex1"));
            }
        }

        /// <summary>
        ///     The Center of the Arc
        /// </summary>
        public Vertex CenterVertex
        {
            get => _centerVertex;
            set
            {
                _centerVertex = value;
                OnGeometryChanged(new GeometryChangedHandlerArgs("CenterVertex"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("CenterVertex"));
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
                OnGeometryChanged(new GeometryChangedHandlerArgs("BulgeValue"));
                UpdateGeometry(this, new GeometryChangedHandlerArgs("BulgeValue"));
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public Vector ToVector()
        {
            // Todo: Test This
            return new Vector(_vertex0, _vertex1);
        }

        /// <inheritdoc />
        /// <summary>
        ///     The Length of the GeoArc
        /// </summary>
        /// <returns>A double which represents the length</returns>
        protected override double CalcLength()
        {
            return GeoMath.Distance(
                _vertex0, _vertex1, _bulge);
        }

        /// <summary>
        ///     Calculates the Center Vertex
        /// </summary>
        /// <returns></returns>
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

                    // Get only properties
                    Length = GeoMath.Distance(_vertex0, _vertex1, _bulge);
                    Area = GeoMath.ChordArea(this);
                }
                    break;
                case "Angle":
                {
                    // Update properties
                    _endAngle = _startAngle + _angle;
                    _bulge = Bulge.CalcBulge(_angle);

                    _vertex1 = CalcPointOnArc(_centerVertex, _endAngle, _radius);
                    _vertex1.GeometryChanged += OnVertexPropertyChanged;

                    Length = GeoMath.Distance(_vertex0, _vertex1, _bulge);
                    Area = GeoMath.ChordArea(this);
                }
                    break;
                case "Radius":
                {
                    _bulge = Math.Tan(_angle / 4);

                    _vertex0 = CalcPointOnArc(_centerVertex, _startAngle, _radius);
                    _vertex0.GeometryChanged += OnVertexPropertyChanged;

                    _vertex1 = CalcPointOnArc(_centerVertex, _endAngle, _radius);
                    _vertex1.GeometryChanged += OnVertexPropertyChanged;

                    // Get only properties
                    Length = GeoMath.Distance(_vertex0, _vertex1, _bulge);
                    Area = GeoMath.ChordArea(this);
                }
                    break;
                case "StartAngle":
                {
                    _angle = _endAngle - _startAngle;

                    _vertex0 = CalcPointOnArc(_centerVertex, _startAngle, _radius);
                    _vertex0.GeometryChanged += OnVertexPropertyChanged;

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

                    // Get only properties
                    Length = GeoMath.Distance(_vertex0, _vertex1, _bulge);
                    Area = GeoMath.ChordArea(this);
                }
                    break;
                case "Vertex0":
                {
                    // todo: Add this
                }
                    break;
                case "Vertex1":
                {
                    // todo: add this
                }
                    break;
                case "CenterVertex":
                {
                    // todo: add this
                }
                    break;
                case "BulgeValue":
                {
                    // todo: add this
                }
                    break;
            }
        }
    }
}