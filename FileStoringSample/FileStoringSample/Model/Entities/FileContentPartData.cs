using FileStoringSample.Model.Abstractions;

namespace FileStoringSample.Model.Entities
{
    public class FileContentPartData : BaseEntity
    {
        /// <summary>
        /// Содержание
        /// </summary>
        public byte[] Bytes { get; set; }
    }
}
