using FileStoringSample.Context;
using FileStoringSample.Model.Entities;
using Microsoft.EntityFrameworkCore;
using PartialStreams.Database;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace FileStoringSample
{
    /// <summary>
    /// Читатель данных из бд
    /// </summary>
    public class DbPartialReader : IDbPartialReader
    {
        private readonly FileContent mFileContent;
        private FileContentPart mCurrentPart;
        private DbDataReader mCurrentPartReader;
        private long mCurrentPartPosition;

        /// <summary>
        /// Контекст БД
        /// </summary>
        private IDataContext Context { get; }

        public DbPartialReader(IDataContext context, FileContent fileContent)
        {
            Context = context;
            mFileContent = fileContent;
        }

        /// <summary>
        /// Инициализирует ридер
        /// </summary>
        public void Init()
        {
            Seek(0);
        }

        /// <inheritdoc />
        public bool CanSeek => true;

        /// <inheritdoc />
        public bool HasParts => mCurrentPart != null;

        /// <inheritdoc />
        public long Position { get; set; }

        /// <inheritdoc />
        public long TotalLength => mFileContent.CurrentSize;

        /// <inheritdoc />
        public bool CanReadFromCurrentPart => mCurrentPartPosition < mCurrentPart.Length;

        /// <inheritdoc />
        public void Seek(long streamOffset)
        {
            var part = Context.Set<FileContentPart>().AsNoTracking()
                .FirstOrDefault(x => x.ContentId == mFileContent.Id && x.Position <= streamOffset && x.Position + x.Length > streamOffset);

            if (part == null)
                throw new InvalidOperationException($"Не удается произвести смещение на позицию {streamOffset}");

            SetCurrentPart(part);
            mCurrentPartPosition = streamOffset - part.Position;
            Position = streamOffset;
        }

        /// <inheritdoc />
        public int Read(byte[] buffer, int offset, int count)
        {
            var readCount = count;
            if (readCount > mCurrentPart.Length - mCurrentPartPosition)
                readCount = (int)(mCurrentPart.Length - mCurrentPartPosition);

            readCount = (int)mCurrentPartReader.GetBytes(0, mCurrentPartPosition, buffer, offset, readCount);

            mCurrentPartPosition += readCount;
            Position += readCount;
            return readCount;
        }

        /// <inheritdoc />
        public bool Next()
        {
            if (mCurrentPart == null)
                return false;

            DisposeCurrentReader();
            var part = Context.Set<FileContentPart>().AsNoTracking()
                .FirstOrDefault(x => x.ContentId == mFileContent.Id && x.Order == mCurrentPart.Order + 1);

            SetCurrentPart(part);
            mCurrentPartPosition = 0;
            Position = mCurrentPart?.Position ?? TotalLength;

            return HasParts;
        }

        /// <summary>
        /// Делает текущей частью указанный <see cref="FileContentPart"/>
        /// </summary>
        private void SetCurrentPart(FileContentPart part)
        {
            mCurrentPart = part;
            CreateReaderForCurrentPart();
        }

        /// <summary>
        /// Получает <see cref="DbDataReader"/> для текущей части
        /// </summary>
        private void CreateReaderForCurrentPart()
        {
            if (mCurrentPart != null)
            {
                mCurrentPartReader = GetReaderForPart(mCurrentPart);
            }
        }

        /// <summary>
        /// Диспозит текущий ридер высвобождая ресурсы
        /// </summary>
        private void DisposeCurrentReader()
        {
            if (mCurrentPartReader != null)
            {
                mCurrentPartReader.Close();
                mCurrentPartReader.Dispose();
                mCurrentPartReader = null;
            }
        }

        /// <summary>
        /// Получает <see cref="DbDataReader"/> для указанной части
        /// </summary>
        protected virtual DbDataReader GetReaderForPart(FileContentPart part)
        {
            var connection = Context.Database.GetDbConnection();

            var command = connection.CreateCommand();
            command.CommandText = @"SELECT Data FROM ""FileContentParts"" WHERE ""Id""=@id";

            var id = command.CreateParameter();
            id.ParameterName = "@id";
            id.DbType = DbType.Guid;
            id.Value = part.Id;

            command.Parameters.Add(id);

            var closedConnection = !connection.State.HasFlag(ConnectionState.Open);
            if (closedConnection)
                connection.Open();

            try
            {
                var parameters =
                    CommandBehavior.SequentialAccess |
                    CommandBehavior.SingleResult |
                    CommandBehavior.SingleRow;

                if (closedConnection)
                    parameters |= CommandBehavior.CloseConnection;

                var reader = command.ExecuteReader(parameters);

                if (!reader.Read())
                    return null;

                closedConnection = false;

                return reader;
            }
            finally
            {
                if (closedConnection)
                    connection.Close();
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            DisposeCurrentReader();
        }

    }
}