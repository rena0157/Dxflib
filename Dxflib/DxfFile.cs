// Dxflib
// DxfFile.cs
// 
// ============================================================
// 
// Created: 2018-08-03
// Last Updated: 2018-08-05-8:53 AM
// By: Adam Renaud
// 
// ============================================================

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dxflib.AcadEntities;
using Dxflib.DxfStream;
using Dxflib.Entities;
using Dxflib.Parser;

// Test Commit Message: Hello

namespace Dxflib
{
    /// <summary>
    /// The DxfFile Class
    /// </summary>
    public class DxfFile
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly DxfFileMainParser _mainParser;

        #region Constructors
        /// <summary>
        ///     Constructor that requires a path to a file.
        ///     This constructor will read the file and set up all of the required
        ///     tools for the DxfFile class
        /// </summary>
        /// <param name="pathToFile">An absolute or relative path to a dxf file</param>
        public DxfFile(string pathToFile)
        {
            // Initalize
            var fileReader = new DxfReader(pathToFile);
            Layers = new LayerDictionary();

            // Setup file
            PathToFile = fileReader.PathToFile;
            FileName = Path.GetFileName(PathToFile);

            // Read and parse the file
            ContentStrings = fileReader.ReadFile();
            Entities = new List<Entity>();
            _mainParser = new DxfFileMainParser(this);
            Layers.UpdateDictionary(Entities);
        }
        #endregion
        
        #region DxfFileContents
        /// <summary>
        /// The Content strings
        /// </summary>
        public string[] ContentStrings { get; }

        /// <summary>
        /// The Layer Dictionary
        /// </summary>
        public LayerDictionary Layers { get; }

        /// <summary>
        /// All of the entities that are extracted
        /// </summary>
        public List<Entity> Entities { get; }
        #endregion

        #region FileProperties
        /// <summary>
        ///     The absolute path to the file
        /// </summary>
        public string PathToFile { get; set; }

        /// <summary>
        ///     The filename and the extension
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Get Entities by Entity Type
        /// </summary>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public List<T> GetEntitiesByType<T>(EntityTypes entityType)
        {
            var returnList = new List<Entity>();
            foreach (Entity entity in Entities)
            {
                if (entity.EntityType == entityType)
                    returnList.Add(entity);
            }

            return returnList.Cast<T>().ToList();
        }
        #endregion

        #region HeaderProperties
        /// <summary>
        /// The Files AutoCAD Version
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public AutoCadVersions AutoCADVersion { get; set; }

        /// <summary>
        /// The Current Layer
        /// </summary>
        public Layer CurrentLayer { get; set; }

        /// <summary>
        /// Who the file was last saved by
        /// </summary>
        public string LastSavedBy { get; set; }
        #endregion

    }
}