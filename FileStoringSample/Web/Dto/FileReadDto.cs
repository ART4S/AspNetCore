using System.IO;

namespace FileStoringSample.Web.Dto
{
    public class FileReadDto
    {
        public string Name { get; set; }

        public string DataType { get; set; }

        public Stream Data { get; set; }
    }
}
