using System.Collections.Generic;
using FileStoringSample.Data.Model.Abstractions;

namespace FileStoringSample.Data.Model.Entities
{
    public class FileContent : BaseEntity
    {
        /// <summary>
        /// Текущий размер загруженных данных
        /// </summary>
        public long CurrentSize { get; set; }

        /// <summary>
        /// Максимальный размер загруженных данных
        /// </summary>
        public long MaxSize { get; set; }

        /// <summary>
        /// Части данных
        /// </summary>
        public virtual ICollection<FileContentPart> Parts { get; } = new HashSet<FileContentPart>();
    }
}
