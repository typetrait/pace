using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Server.Model
{
    public class File
    {
        public string Name { get; set; }
        public int Size { get; set; }

        public File(string name)
        {
            Name = name;
            Size = 0;
        }
    }
}