# Dxflib - The .NET Dxf Library

## Introduction
The Dxf file format stands for Drawing Interchange Format. It is a CAD data 
file format developed by AutoDESK, the creators of AutoCAD. It was designed to create data
interoperabilty between AutoCAD and other programs. Dxf was originally introduced in 1982 (a very
long time ago). The file is broken down into different sections, namely header, classes, tables, blocks
entities, and a thumbnail image section. 

## Dxf File Basics
Data is stored in a "Tagged" format. For example here is a section of a DXF file that contains
some information about a LWPOLYLINE entity.

```
LWPOLYLINE // Start Marker
  5 // Group Code - handle
2C1 // Group Code value - The handle value
330
1F
100
AcDbEntity
  8
0
100
AcDbPolyline
 90
        4
 70
     1
 43
0.0
 10
0.0
```

In the above section I included some comments, the first line is what I call the start marker
for an entity type. It is the entity's name. The line where the second comment is located is called
a group code. A group code is a numberical value that 1 determines the type of the next line - which
is the actual value of the group codes element - and the value type. For example the group code
that I highlighted is for the handle of the Entity. The value is `2C1`. 

This pattern of group code followed by group code value is the basis of the dxf file's system for
organizing information.

## Development 
The goal of the development for the dxf library is to provide a programming interface in the 
.NET framework for the DXF platform. Development for the library has started in many different 
forms and has been written and rewritten in many different languages. I started developing the 
library in python, then it moved to rust, then C++, and not C#. I started developing the library 
to help me at my job where I was a drafts person. I found that during drafting there is often 
very repetative tasks that ought to be done by a computer. I know that AutoCAD has its own 
.NET framework that is infinitely more up-to-date than this library, but it requires a premium
license. Also, there are other reasons for developing a dxf library that completely bypass AutoCAD
there are other drafting applications that also use the file type. Also, because of the functionality
required for a drafting library it can also one day be used to create drawings without the use
of AutoCAD or other drafting applications.

