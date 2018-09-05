// Dxflib
// AsciiParser.cs
// 
// ============================================================
// 
// Created: 2018-08-26
// Last Updated: 2018-09-01-1:09 PM
// By: Adam Renaud
// 
// ============================================================

using System.IO;
using Dxflib.AcadEntities;
using Dxflib.AcadEntities.Pointer;
using Dxflib.IO.Header;

#pragma warning disable 414 // There was annoying error that was ignored here

namespace Dxflib.IO
{
    /// <summary>
    ///     The ASCII Parser is the one of the main parsers in the Dxflib Library.
    ///     This class is used to extract data from a ASCII dxf file. The main constructor
    ///     requires a <see cref="DxfFile" /> and when <see cref="ParseFile" /> is called
    ///     will populate all of the fields in the <see cref="DxfFile" />.
    /// </summary>
    public class AsciiParser
    {
        private readonly DxfFile _dxfFile;
        private EntitiesSectionArgs _entitiesSectionArgs;
        private HeaderSectionArgs _headerSectionArgs;

        /// <summary>
        ///     The main constructor. This constructor will set the target dxf file.
        /// </summary>
        /// <param name="dxfFile">
        ///     The dxf file that data will be extracted from.
        ///     All of the fields will be populated.
        /// </param>
        public AsciiParser(DxfFile dxfFile) { _dxfFile = dxfFile; }

        /// <summary>
        ///     This function will parse the file. It will populate all of the fields of the
        ///     dxf file that was passed to the constructor.
        /// </summary>
        public void ParseFile()
        {
            // Throw an exception if the file is empty
            if ( _dxfFile.DxfFileData.Length == 0 )
                throw new FileLoadException("The File is not a valid Dxf file or is empty");

            // Iterate through the tagged data list
            for ( var currentIndex = 0;
                currentIndex < _dxfFile.DxfFileData.Length;
                ++currentIndex )
            {
                // The current tagged data pair
                var currentData = _dxfFile.DxfFileData.GetPair(currentIndex);
                switch ( currentData.Value )
                {
                    /*
                     * In each switch statement the following pattern is followed
                     * 1. The Current section is set to the case name
                     * 2. The SectionArgs is initialized and then is populated
                     *    and is built using a build private function
                     * 3. When a new section header is reached the process is started over again
                     */
                    case FileSectionStartMarkers.Header:
                        _headerSectionArgs
                            = new HeaderSectionArgs(currentIndex, _dxfFile.DxfFileData);
                        _headerSectionArgs.ReadSection();
                        BuildHeader();
                        continue;
                    case FileSectionStartMarkers.Tables:
                        continue;
                    case FileSectionStartMarkers.Blocks:
                        continue;
                    case FileSectionStartMarkers.Entities:
                        _entitiesSectionArgs = new EntitiesSectionArgs(currentIndex, _dxfFile.DxfFileData);
                        _entitiesSectionArgs.ReadSection();
                        BuildEntities();
                        continue;
                    case FileSectionStartMarkers.Objects:
                        continue;
                    default:
                        continue;
                }
            }
        }

        /// <summary>
        ///     The Header Build Private function
        /// </summary>
        private void BuildHeader()
        {
            _dxfFile.AutoCADVersion = _headerSectionArgs.AutoCadVersion.Value;
            _dxfFile.LastSavedBy = _headerSectionArgs.LastSavedBy.Value;
            _dxfFile.CurrentLayer = new Layer(_headerSectionArgs.CurrentLayer.Value);
        }

        /// <summary>
        ///     The Entities Build Private Function
        /// </summary>
        private void BuildEntities()
        {
            _dxfFile.Entities = new EntityCollection(_entitiesSectionArgs.Entities);

            // Link all referenced Entities
            foreach ( var entity in _dxfFile.Entities.Values )
            {
                if ( !entity.HasReferencedEntities )
                    continue;

                foreach ( var entityPointer in entity.ReferencedEntities )
                    if ( _dxfFile.Entities.ContainsKey(entityPointer.Handle) )
                        entityPointer.RefEntity = _dxfFile.Entities[entityPointer.Handle];
                    else
                        throw new EntityPointerException("Pointer Handle was not found");

                entity.UpdateReferencedEntities();
            }
        }
    }
}