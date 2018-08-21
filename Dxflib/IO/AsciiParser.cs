// Dxflib
// AsciiParser.cs
// 
// ============================================================
// 
// Created: 2018-08-19
// Last Updated: 2018-08-19-7:49 PM
// By: Adam Renaud
// 
// ============================================================

using System;
using Dxflib.Tools;

namespace Dxflib.IO
{
    /// <summary>
    /// 
    /// </summary>
    public class AsciiParser
    {
        private FileSections _currentSection;
        private HeaderSectionArgs _headerSectionArgs;
        private EntitiesSectionArgs _entitiesSectionArgs;
        private DxfFile _dxfFile;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dxfFile"></param>
        public AsciiParser(DxfFile dxfFile) { _dxfFile = dxfFile; }

        /// <summary>
        /// Parse the file
        /// </summary>
        public void ParseFile()
        {
            for ( var currentData = _dxfFile.DxfFileData.GetPair(0);
                _dxfFile.DxfFileData.Index < _dxfFile.DxfFileData.Length;
                currentData = _dxfFile.DxfFileData.Next)
            {

                if ( currentData.Value == DxflibTools
                         .GetEnumDescription(FileSections.Header) )
                {
                    _currentSection = FileSections.Header;
                    _headerSectionArgs 
                        = new HeaderSectionArgs(_dxfFile.DxfFileData.Index, _dxfFile.DxfFileData);
                    _headerSectionArgs.ReadSection();
                    BuildHeader();
                }
                else if ( currentData.Value == DxflibTools
                              .GetEnumDescription(FileSections.Tables) )
                {
                    _currentSection = FileSections.Tables;
                }
                else if ( currentData.Value == DxflibTools
                              .GetEnumDescription(FileSections.Blocks) )
                {
                    _currentSection = FileSections.Blocks;
                }
                else if ( currentData.Value == DxflibTools
                              .GetEnumDescription(FileSections.Entities) )
                {
                    _currentSection = FileSections.Entities;
                    _entitiesSectionArgs = new EntitiesSectionArgs(_dxfFile.DxfFileData.Index, _dxfFile.DxfFileData);
                    _entitiesSectionArgs.ReadSection();
                    BuildEntities();

                }
                else if ( currentData.Value == DxflibTools
                              .GetEnumDescription(FileSections.Objects) )
                {
                    _currentSection = FileSections.Objects;
                }
            }
        }

        private void BuildHeader()
        {
            _dxfFile.AutoCADVersion = _headerSectionArgs.AutoCadVersion.Value;
            _dxfFile.LastSavedBy = _headerSectionArgs.LastSavedBy.Value;
        }

        private void BuildEntities()
        {
            _dxfFile.Entities = _entitiesSectionArgs.Entities;
        }
    }
}