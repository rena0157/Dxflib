# Change Log

This is the change log file that will contain all of the information regarding
releases of the software. The list will be a decending list from newest to oldest releases.

## Version 0.4.0 [2018-09-XX]
- Renamed `GeometricEntityBase` to `GeoBase` to shorten the name
- Inheriting the `INotifyPropertyChanged` Interface from the `GeoBase` class
  - Changed most of the underlying structure for how `PropertyChanged` events are handled in the GeoBase class
- Removed a lot of classes that were made obsolete due to the change to the `INotifyPropertyChanged` event system

## Version 0.3.0 [2018-09-01]
- Added the Hatch Entity and associated buffer classes
  - Added Hatch Styles Enum
  - Added Hatch Pattern Types Enum
  - Added Hatch Boundary object types Enum
- Added the CircularArc Entity and associated buffer classes
- Created a base constructor for the entitybase class that should simplify futher entity creation
- Reorganized some of the classes into different files for the entity class
- Added the EntitiesCollection Class
- Changed the Entities List to an EntitiesCollection object that will add more specialization to the collection
  - Moved the GetEntitiesByType Function to the Entities Property in the DxfFile Class
- Added the EntityPointer Class
  - All entities now have the ability to point to other entities (Entity References). This is currently being used in the hatch.
- Added the LinePointer Class: A class that allows another Entity to point to a Line Entity
- Added the LwpolylinePointer Class: A class that allows another Entity to point to an lwpolyline
- Added some more logic to the GeoPolyline class that can now account for both counterclockwise and clockwise section creation
  - Added isCounterClockwise Property to the GeoPolyline
- Added Equals override to the Vertex Class, now you can compare verticies using this function
- Added GetHashCode to the Vertex Class
- Added Parse Exception Class

## Version 0.2.0
- Updated the parsing engine
  - The new parsing engine should allow the api to read ASCII files at twice the previous version speed
- Starting work on the Hatch entity.

## Version 0.1.0
- First Public Release