using System;
using System.Collections.Generic;
using System.Text;

namespace Dxflib.AcadEntities
{
    public class Layer
    {
        public Layer(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
