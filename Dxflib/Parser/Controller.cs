using System;
using Dxflib.IO;

namespace Dxflib.Parser
{
    /// <summary>
    /// The Controller class for parsing. The controller class controls the root of the
    /// parsing tree
    /// </summary>
    public class Controller
    {
        private readonly DxfFileMainParser _thisParser;

        /// <summary>
        /// Main Constructor for the Controller class
        /// </summary>
        /// <param name="thisParser">The Main Parser that the controller will control</param>
        public Controller(DxfFileMainParser thisParser)
        {
            thisParser.LineChanged += ThisParserOnLineChanged;
            _thisParser = thisParser;
        }

        private void ThisParserOnLineChanged(object sender, LineChangeHandlerArgs args)
        {
            switch (args.NewCurrentLine)
            {
                case FileSectionStrings.SectionEnd:
                    break;
                case FileSectionStrings.Header:
                    _thisParser.CurrentFileSection = FileSections.Header;
                    break;
                case FileSectionStrings.Classes:
                    _thisParser.CurrentFileSection = FileSections.Classes;
                    break;
                case FileSectionStrings.Blocks:
                    _thisParser.CurrentFileSection = FileSections.Blocks;
                    break;
                case FileSectionStrings.Entities:
                    _thisParser.CurrentFileSection = FileSections.Entities;
                    break;
                case FileSectionStrings.Objects:
                    _thisParser.CurrentFileSection = FileSections.Objects;
                    break;
            }

            switch (_thisParser.CurrentFileSection)
            {
                case FileSections.Header:
                    HeaderParser.Parse(_thisParser, args);
                    break;
                case FileSections.Classes:
                    break;
                case FileSections.Tables:
                    break;
                case FileSections.Blocks:
                    break;
                case FileSections.Entities:
                    EntityParser.Parse(_thisParser, args);
                    break;
                case FileSections.Objects:
                    break;
                case FileSections.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}