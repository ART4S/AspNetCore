using FileStoringSample.Data.Model.Abstractions;

namespace FileStoringSample.Data.Model.Entities
{
    public class FileContentPartData : BaseEntity
    {
        /// <summary>
        /// Содержание
        /// </summary>
        public byte[] Bytes { get; set; }
    }
}
