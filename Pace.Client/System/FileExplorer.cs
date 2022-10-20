using Pace.Common.Model;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Pace.Client.System;

public static class FileExplorer
{
    public static FileSystemEntry[] GetDirectories(string path)
    {
        var info = new DirectoryInfo(path);
        var dirs = new List<string>();

        return info.GetDirectories().Select((dir) => new FileSystemEntry(dir.Name, dir.FullName, 0, FileType.Directory)).ToArray();
    }

    public static FileSystemEntry[] GetFiles(string path)
    {
        var info = new DirectoryInfo(path);
        var files = new List<string>();

        return info.GetFiles().Select((file) => new FileSystemEntry(file.Name, file.FullName, file.Length, FileType.File)).ToArray();
    }
}