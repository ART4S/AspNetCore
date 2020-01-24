using FileStoringSample.Model.Abstractions;
using System;

namespace FileStoringSample.Model.Entities
{
    public class FileContentPart : BaseEntity
    {
        /// <summary>
        /// Содержимое
        /// </summary>
        public Guid ContentId { get; set; }
        public virtual FileContent Content { get; set; }

        /// <summary>
        /// Номер части
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Длина части
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Позиция части в файле
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Содержание части файла
        /// </summary>
        public Guid DataId { get; set; }
        public virtual FileContentPartData Data { get; set; }
    }
}
