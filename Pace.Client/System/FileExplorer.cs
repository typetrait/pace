using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Client.System
{
    public static class FileExplorer
    {
        public static string[] GetDirectories(string path)
        {
            var info = new DirectoryInfo(path);
            var dirs = new List<string>();

            return info.GetDirectories().Select((dir) => dir.FullName).ToArray();
        }

        public static string[] GetFiles(string path)
        {
            var info = new DirectoryInfo(path);
            var files = new List<string>();

            return info.GetFiles().Select((file) => file.FullName).ToArray();
        }
    }
}