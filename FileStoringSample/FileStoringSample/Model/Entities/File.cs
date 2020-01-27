using System;
using FileStoringSample.Data.Model.Abstractions;

namespace FileStoringSample.Data.Model.Entities
{
    public class File : BaseEntity
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Размер файла
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Дата создания файла
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Содержимое
        /// </summary>
        public Guid ContentId { get; set; }
        public virtual FileContent Content { get; set; }
    }
}
