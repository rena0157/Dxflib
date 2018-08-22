// Dxflib
// DxfReader.cs
// 
// ============================================================
// 
// Created: 2018-08-03
// Last Updated: 2018-08-03-7:04 PM
// By: Adam Renaud
// 
// ============================================================
// 
// Purpose:

using System;
using System.IO;

namespace Dxflib.DxfStream
{
    /// <summary>
    /// The DxfReader class that reads a Dxf file and returns a string list
    /// </summary>
    public class DxfReader
    {
        /// <summary>
        ///     Creates an istance of the DxfReader class. Takes a file path as input and reads the file
        ///     The file contents can be read using the readfile method
        /// </summary>
        /// <param name="pathToFile"></param>
        public DxfReader(string pathToFile)
        {
            PathToFile = pathToFile;
        }

        /// <summary>
        ///     The Path to the file on the disk
        /// </summary>
        public string PathToFile { get; }

        /// <summary>
        ///     Reads all of the contents of the dxf file into a list of strings
        /// </summary>
        /// <returns>A List of strings</returns>
        public string[] ReadFile()
        {
            // Check if the file exists
            if (!File.Exists(PathToFile))
                throw new DxfStreamException("File does not exist or was not found");

            // make sure that the file is a dxf file
            if (Path.GetExtension(PathToFile) != ".dxf")
                throw new DxfStreamException("The file extension must be .dxf");

            // The file stream and stream reader that is used to access the file system
            var fs = new FileStream(PathToFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(fs);

            var allText = sr.ReadToEnd().Split(new[] {Environment.NewLine}, StringSplitOptions.None);


            // Closing the stream instances
            sr.Close();
            fs.Close();

            return allText;
        }
    }
}