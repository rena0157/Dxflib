using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Dxflib.Parser
{
    public class DxfFileMainParser
    {
        /// <summary>
        /// The event broadcasts when the main parser clicks over to the next iteration
        /// </summary>
        public event LineChangeHandler LineChanged;

        // The dxf file that was passed by the constructor
        public DxfFile ThisFile;

        // The current file section
        public FileSection CurrentFileSection
        {
            get;
            set;
        }

        /// <summary>
        /// Constructor that sets the dxf file and creates and manages all of the other parsers
        /// </summary>
        /// <param name="dxfFile"></param>
        public DxfFileMainParser(DxfFile dxfFile)
        {
            ThisFile = dxfFile;
            Controller controller = new Controller(this);
            IterateThroughFile();
        }

        /// <summary>
        /// Invokation of the LineChanged event
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnLineChanged(LineChangeHandlerArgs args)
        {
            LineChanged?.Invoke(this, args);
        }

        // Iterate through the file
        private void IterateThroughFile()
        {
            for (int lineIndex = 0; lineIndex < ThisFile.ContentStrings.Count - 1; ++lineIndex)
            {
                // updating the current and next lines
                var currentLine = ThisFile.ContentStrings[lineIndex];
                var nextLine = ThisFile.ContentStrings[lineIndex + 1];

                // Broadcast the event
                OnLineChanged(new LineChangeHandlerArgs(currentLine, nextLine, lineIndex));
            }
        }
    }



    /// <summary>
    /// The Delegate for the Line changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void LineChangeHandler(object sender, LineChangeHandlerArgs args);

    /// <summary>
    /// The arguments that are passed to the delegate for the LineChangedHandler
    /// </summary>
    public class LineChangeHandlerArgs
    {
        public LineChangeHandlerArgs(string newCurrentLine, string newNextLine, int lineIndex)
        {
            NewCurrentLine = newCurrentLine;
            NewNextLine = newNextLine;
            LineIndex = lineIndex;
        }

        public string NewCurrentLine { get; }
        public string NewNextLine { get; }
        public int LineIndex { get; }
    }

    public enum FileSection
    {
        [Description("Header")]
        Header,
        
        [Description("Classes")]
        Classes,
        
        [Description("Tables")]
        Tables,
        
        [Description("Blocks")]
        Blocks,

        [Description("Entities")]
        Entities,

        [Description("Objects")]
        Objects,

        [Description("None")]
        None
    }

    public struct FileSectionStrings
    {
        public const string SectionStart = "SECTION";
        public const string SectionEnd = "ENDSEC";
        
        public const string Header = "HEADER";
        public const string Classes = "CLASSES";
        public const string Tables = "TABLES";
        public const string Blocks = "BLOCKS";
        public const string Entities = "Entities";
        public const string Objects = "OBJECTS";
    }
}
