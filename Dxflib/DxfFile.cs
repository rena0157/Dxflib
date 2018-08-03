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
            ContentStrings = _fileReader.ReadFile();
        }

        public string PathToFile { get; set; }
        public List<string> ContentStrings { get; }

        private DxfReader _fileReader;

    }
}