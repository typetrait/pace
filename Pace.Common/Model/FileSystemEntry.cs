namespace Pace.Common.Model
{
    public class FileSystemEntry
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public FileType Type { get; set; }

        public FileSystemEntry(string name, string path, long size, FileType type)
        {
            Name = name;
            Path = path;
            Size = size;
            Type = type;
        }
    }
}