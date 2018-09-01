# DXF Library - The Drawing Interchange Format Library in C#

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/310014992d7743078a5d2d57e79ce44c)](https://www.codacy.com/project/muckanee/Dxflib/dashboard?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=rena0157/Dxflib&amp;utm_campaign=Badge_Grade_Dashboard)

### Introduction
This is one of many DXF librarys that is avalible here on Github. It can be used currently to extract and read information from 
a dxf file. One of the main focuses however besides being able to read and write a file is to be able to calculate all of the geometry
of the entities in the file. Also, this library is designed to be flexible, in other words if a property is changed in an entity all the
geometry will be updated along will the geometry of all entities that are linking to the object. 

#### Goals of this Library
- read and write all of the entities in the Dxf format
- flexible geometry based entities that can be updated real time without writing and re reading the file.
- Speed, this library should be really fast.

Check out my website for the application on github-pages: [DXF Site](https://rena0157.github.io/Dxflib/)

Also, take a look at the change log [Change Log](https://rena0157.github.io/Dxflib/articles/ChangeLog.html)

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
var lines = dxfFile.Entities.GetEntitiesByType<Line>(EntityTypes.Line);

// When a GeometryChange event is called Geometry will Automatically update
lines[0].Vertex = new Vertex(0, 1); // GeometryChanged Event will be called at the Vertex Entity
```

### Currently Supported Entities
- LINE
- LWPOLYLINE
- HATCH
- ARC

### Future Development
Future development includes adding these other entities (in order of importance):
- TEXT
- MTEXT
- POLYLINE