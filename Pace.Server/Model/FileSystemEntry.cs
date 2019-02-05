namespace Pace.Server.Model
{
    public class FileSystemEntry
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public FileType Type { get; set; }

        public FileSystemEntry(string name, long size, FileType type)
        {
            Name = name;
            Size = size;
            Type = type;
        }
    }
}