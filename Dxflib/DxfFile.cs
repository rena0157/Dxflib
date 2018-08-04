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
using Dxflib.AcadEntities;
using Dxflib.DxfStream;
using Dxflib.Parser;
using Dxflib.Entities;

namespace Dxflib
{
    public class DxfFile
    {
        #region Constructors

        /// <summary>
        /// Constructor that requires a path to a file.
        /// This constructor will read the file and set up all of the required
        /// tools for the DxfFile class
        /// </summary>
        /// <param name="pathToFile">An absolute or relative path to a dxf file</param>
        public DxfFile(string pathToFile)
        {
            // Initalize
            _fileReader = new DxfReader(pathToFile);
            Layers = new LayerDictionary();

            // Setup file
            PathToFile = _fileReader.PathToFile;
            FileName = Path.GetFileName(PathToFile);
            
            // Read and parse the file
            ContentStrings = _fileReader.ReadFile();
            Entities = new List<Entity>();
            var mainParser = new DxfFileMainParser(this);
        }
        #endregion

        #region DxfFileContents
        
        public string[] ContentStrings { get; }
        public LayerDictionary Layers { get; set; }
        public List<Entity> Entities;

        #endregion

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

        #region HeaderProperties

        public AutoCADVersions AutoCADVersion { get; set; }
        public Layer CurrentLayer { get; set; }
        public string LastSavedBy { get; set; }

        #endregion

        private DxfReader _fileReader;

    }
}