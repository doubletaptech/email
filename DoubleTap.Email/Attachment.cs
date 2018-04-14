namespace DoubleTap.Email
{
    public class Attachment
    {
        public Attachment(string name, string filePath)
        {
            Name = name;
            FilePath = filePath;
        }

        public string Name { get; }
        public string FilePath { get; }
    }
}