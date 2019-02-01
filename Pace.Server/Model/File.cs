namespace Pace.Server.Model
{
    public class File
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public FileType Type { get; set; }

        public File(string name, long size, FileType type = FileType.File)
        {
            Name = name;
            Size = size;
            Type = type;
        }
    }
}