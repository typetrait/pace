namespace Pace.Server.Model
{
    public class File
    {
        public string Name { get; set; }
        public long Size { get; set; }

        public File(string name, long size)
        {
            Name = name;
            Size = size;
        }
    }
}