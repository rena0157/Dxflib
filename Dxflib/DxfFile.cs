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
    /// The DxfFile Class: is a class that is designed to replicate the properties and methods
    /// of a DXF file. It is the root class for all entities and is required for reading a file.
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
        /// The Content strings are all of the lines from the dxf file in a list of strings
        /// </summary>
        public string[] ContentStrings { get; }

        /// <summary>
        /// The Layer Dictionary is a <see cref="Dxflib.AcadEntities.LayerDictionary"/> object that is
        /// designed to hold all of the layers.
        /// </summary>
        public LayerDictionary Layers { get; }

        /// <summary>
        /// The Entities property is a list of all the entities that were read from the file. The
        /// <see cref="Entity"/> types can be changed into any other type of derived class.
        /// </summary>
        public List<Entity> Entities { get; }
        #endregion

        #region FileProperties
        /// <summary>
        ///     The absolute path to the file that is read.
        /// </summary>
        public string PathToFile { get; set; }

        /// <summary>
        ///     The filename and the extension of the file that was read.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Get Entities by Entity Type returns a list of entities
        /// by the type that was selected. See <see cref="Entity"/>.
        /// </summary>
        /// <param name="entityType">The <see cref="EntityTypes"/> that will be placed into the list</param>
        /// <returns>A list of entities all members of the type <see cref="EntityTypes"/></returns>
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
        /// The Files AutoCAD Version <see cref="AutoCadVersions"/>
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public AutoCadVersions AutoCADVersion { get; set; }

        /// <summary>
        /// The Current Layer of the file. This is the last
        /// layer that was selected before the file was saved.
        /// </summary>
        public Layer CurrentLayer { get; set; }

        /// <summary>
        /// The username of the person who the file was last saved by
        /// </summary>
        public string LastSavedBy { get; set; }
        #endregion

    }
}