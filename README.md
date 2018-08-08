# DXF Library - The Drawing Exchange Format Library in C#

### Introduction
This is one of many DXF librarys that is avalible here on Github. This library serves a dual purpose:
one is to help me learn the .NET framework and the second is in the future I hope that this library
will be the backend for an AutoCAD Drafting companion application. This Drafting companion application
will help drafters with the more repetative task without needing to learn lisp.

### Supported Versions
Currently this Library supports the following Versions of AutoCAD
- AC1006 (R10)
- AC1009 (R11 & R12)
- AC1012 (R14)
- AC1014 (R14)
- AC1015 (AutoCAD 2000)
- AC1018 (AutoCAD 2004)
- AC1021 (AutoCAD 2007)
- AC1024 (AutoCAD 2010)
- AC1027 (AutoCAD 2013)

### Example

```
// ExampleFile.cs

// open a file
var dxfFile = new DxfFile(StringPathToFile);

// Get whole layers
var layer = dxfFile.Layers.GetLayer("LayerTest");

// Entities all have a common base class "Entity" so they can all be down and up casted
foreach (var entity in layer.GetAllEntities())
{
	// setting all lwpolylines elevations to 0
	if (entity.EntityType == EntityTypes.LwPolyline && ((LwPolyLine)entity).Elevation != 0)
	{
		((LwPolyLine)entity).Elevation = 0;
	}
}

// Geometry was originally calc. duing extraction
var lines = GetEntitiesByType<Line>(Line);

// When a GeometryChange event is called Geometry will Automatically update
lines[0].Vertex = new Vertex(0, 1); // GeometryChanged Event will be called at the Vertex Entity
```

### Currently Supported Entities
- LINE
- LWPOLYLINE

### Future Development
Future development includes adding these other entities (in order of importance):
- HATCH
- TEXT
- MTEXT
- ARC
- POLYLINE
- SPLINE
- Others to come...

### Contribution
Contribution would really by helpful give me a shout if you would like to contribute or send 
a pull request. I am not that new to coding but I am learning C# and would really appreciate any
input.
