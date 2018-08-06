using System.ComponentModel;
using Dxflib.Entities;

namespace Dxflib.Parser
{
    public class DxfFileMainParser
    {
        /// <summary>
        ///     This is the Dxf file that was passed by the main constructor
        /// </summary>
        public DxfFile ThisFile { get; }

        /// <summary>
        ///     Constructor that sets the dxf file and creates and manages all of the other parsers
        /// </summary>
        /// <param name="dxfFile">A DXF file</param>
        public DxfFileMainParser(DxfFile dxfFile)
        {
            // The Current DxfFile
            ThisFile = dxfFile;
            var controller = new Controller(this);

            // Default Values
            CurrentEntityForExtraction = EntityTypes.None;
            LineBuf = new LineBuffer();
            LwPolyLineBuf = new LwPolyLineBuffer();

            IterateThroughFile();
        }

        /// <summary>
        ///     The Current File section
        ///     Ex: Header, Entities, Objects
        /// </summary>
        public FileSection CurrentFileSection { get; set; }

        /// <summary>
        ///     The Current Entity that is being extracted
        /// </summary>
        public EntityTypes CurrentEntityForExtraction { get; set; }

        /// <summary>
        ///     The Line Buffer where all information is to be stored until the
        ///     Line Entity can be created
        /// </summary>
        public LineBuffer LineBuf { get; set; }

        /// <summary>
        ///     The LwPolyline Buffer where all information is to be stored unil
        ///     the LwPolyline Entity can be created
        /// </summary>
        public LwPolyLineBuffer LwPolyLineBuf { get; set; }

        /// <summary>
        ///     The event broadcasts when the main parser clicks over to the next iteration
        /// </summary>
        public event LineChangeHandler LineChanged;

        /// <summary>
        ///     Invokation of the LineChanged event
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnLineChanged(LineChangeHandlerArgs args)
        {
            LineChanged?.Invoke(this, args);
        }

        /// <summary>
        ///     Function that will iterate through the string[] that is in the
        ///     dxf file and broadcast the event <see cref="LineChanged" /> whenever the line does change
        /// </summary>
        private void IterateThroughFile()
        {
            for (var lineIndex = 0; lineIndex < ThisFile.ContentStrings.Length - 1; ++lineIndex)
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
    ///     The Delegate for the Line changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    public delegate void LineChangeHandler(object sender, LineChangeHandlerArgs args);

    /// <summary>
    ///     The arguments that are passed to the delegate for the LineChangedHandler
    /// </summary>
    public class LineChangeHandlerArgs
    {
        /// <summary>
        ///     Main Constructor for the LineChangeHandlerArgs class
        /// </summary>
        /// <param name="newCurrentLine">The Current line that the iteration is at</param>
        /// <param name="newNextLine">The next line in the list that the iteration is at</param>
        /// <param name="lineIndex">The current line index</param>
        public LineChangeHandlerArgs(string newCurrentLine, string newNextLine, int lineIndex)
        {
            NewCurrentLine = newCurrentLine;
            NewNextLine = newNextLine;
            LineIndex = lineIndex;
        }

        /// <summary>
        ///     The New Current Line
        /// </summary>
        public string NewCurrentLine { get; }

        /// <summary>
        ///     The New Next Line
        /// </summary>
        public string NewNextLine { get; }

        /// <summary>
        ///     The Line Index
        /// </summary>
        public int LineIndex { get; }
    }

    /// <summary>
    ///     The File Section Enumeration that holds Enumerations for
    ///     the different section of a dxf file
    /// </summary>
    public enum FileSection
    {
        [Description("Header")] Header,

        [Description("Classes")] Classes,

        [Description("Tables")] Tables,

        [Description("Blocks")] Blocks,

        [Description("Entities")] Entities,

        [Description("Objects")] Objects,

        [Description("None")] None
    }

    public struct FileSectionStrings
    {
        public const string SectionStart = "SECTION";
        public const string SectionEnd = "ENDSEC";

        public const string Header = "HEADER";
        public const string Classes = "CLASSES";
        public const string Tables = "TABLES";
        public const string Blocks = "BLOCKS";
        public const string Entities = "ENTITIES";
        public const string Objects = "OBJECTS";
    }
}