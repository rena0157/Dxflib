// Dxflib
// DxfFile.cs
// 
// ============================================================
// 
// Created: 2018-08-03
// Last Updated: 2018-08-03-5:55 PM
// By: Adam Renaud
// 
// ============================================================
//

// Purpose: To Hold the functionality of the DxfFile Class

// CLR References
using System;
using System.Collections.Generic;
using System.IO;
using Dxflib.DxfStream;

namespace Dxflib
{
    public class DxfFile
    {
        public DxfFile(string pathToFile)
        {
            _fileReader = new DxfReader(pathToFile);
            PathToFile = _fileReader.PathToFile;
            FileName = Path.GetFileName(PathToFile);
            ContentStrings = _fileReader.ReadFile();
        }

        #region FileProperties

        /// <summary>
        /// The absolute path to the file
        /// </summary>
        public string PathToFile { get; set; }

        /// <summary>
        /// The filename and the extension
        /// </summary>
        public string FileName { get; }
        #endregion

        public List<string> ContentStrings { get; }

        private DxfReader _fileReader;

    }
}