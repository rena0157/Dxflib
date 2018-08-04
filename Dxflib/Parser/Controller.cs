using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Dxflib.Parser
{
    public class Controller
    {
        private DxfFileMainParser _thisParser;

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
                    _thisParser.CurrentFileSection = FileSection.Header;
                    break;
                case FileSectionStrings.Classes:
                    _thisParser.CurrentFileSection = FileSection.Classes;
                    break;
                case FileSectionStrings.Blocks:
                    _thisParser.CurrentFileSection = FileSection.Blocks;
                    break;
                case FileSectionStrings.Entities:
                    _thisParser.CurrentFileSection = FileSection.Entities;
                    break;
                case FileSectionStrings.Objects:
                    _thisParser.CurrentFileSection = FileSection.Objects;
                    break;
                default:
                    break;
            }

            switch (_thisParser.CurrentFileSection)
            {
                case FileSection.Header:
                    HeaderParser.Parse(_thisParser, args);
                    break;
                case FileSection.Classes:
                    break;
                case FileSection.Tables:
                    break;
                case FileSection.Blocks:
                    break;
                case FileSection.Entities:
                    break;
                case FileSection.Objects:
                    break;
                case FileSection.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
