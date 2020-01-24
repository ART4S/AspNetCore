using FileStoringSample.Model.Abstractions;
using System.Collections.Generic;

namespace FileStoringSample.Model.Entities
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
