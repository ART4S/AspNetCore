using FileStoringSample.Data.Context;
using FileStoringSample.Data.Model.Entities;
using PartialStreams.Database;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FileStoringSample.Data.FileAccess
{
    public class DbPartialWriter : IDbPartialWriter
    {
        private readonly int _partSize;

        private IDataContext _dataContext;
        private FileContent _content;
        private FileContentPart _currentPart;
        private int _currentPartPosition;

        public long Position => _currentPart.PositionInFile + _currentPartPosition;
        public long TotalLength => _content.CurrentSize;
        public bool CanSeek => true;
        public bool HasParts => CanWriteBytes();
        public bool CanWriteToCurrentPart => _currentPartPosition + 1 <= _partSize && CanWriteBytes();

        public DbPartialWriter(IDataContext dataContext, FileContent content, int partSize = 1024)
        {
            _dataContext = dataContext;
            _content = content;
            _partSize = partSize;

            _currentPart = _dataContext.Set<FileContentPart>()
                .AsNoTracking()
                .Where(x => x.ContentId == _content.Id)
                .OrderByDescending(x => x.Order)
                .FirstOrDefault();

            if (_currentPart == null)
            {
                _currentPart = new FileContentPart()
                {
                    Content = _content,
                    Data = new FileContentPartData() { Bytes = new byte[_partSize] },
                    PositionInFile = 0,
                    Order = 0,
                    Length = 0,
                };

                _dataContext.Set<FileContentPart>().Add(_currentPart);
            }

            _currentPartPosition = (int) (_content.CurrentSize - _currentPart.PositionInFile);
        }

        public void SetStreamLength(long value) => throw new NotImplementedException();

        public void Seek(long streamOffset)
        {
            _currentPart = _dataContext.Set<FileContentPart>()
                .AsNoTracking()
                .FirstOrDefault(
                    x => x.ContentId == _content.Id &&
                         x.PositionInFile <= streamOffset &&
                         streamOffset < x.PositionInFile + x.Length);

            if (_currentPart == null)
                throw new InvalidOperationException();

            _currentPartPosition = (int) (streamOffset - _currentPart.PositionInFile);
        }

        public int Write(byte[] buffer, int offset, int count)
        {
            int writeCount = Math.Min(
                Math.Min(count, _partSize - _currentPartPosition), 
                (int)(_content.MaxSize - _content.CurrentSize));

            Array.Copy(buffer, offset, _currentPart.Data.Bytes, _currentPartPosition, writeCount);

            _currentPartPosition += writeCount;
            _content.CurrentSize += writeCount;

            return writeCount;
        }

        public bool Next()
        {
            if (_content.CurrentSize == _content.MaxSize)
                return false;

            var newPart = new FileContentPart()
            {
                Content = _content,
                Data = new FileContentPartData() { Bytes = new byte[_partSize] },
                PositionInFile = _currentPart.PositionInFile + _currentPart.Length + 1,
                Order = _currentPart.Order + 1,
                Length = 0
            };

            _dataContext.Set<FileContentPart>().Add(newPart);

            return true;
        }

        public void Flush()
        {
            _dataContext.SaveChanges();
        }

        public void Dispose()
        {
            _dataContext = null;
            _content = null;
            _currentPart = null;
        }

        private bool CanWriteBytes() => _currentPart.PositionInFile + _currentPartPosition + 1 <= _content.MaxSize;
    }

    /// <summary>
    /// Писатель данных в бд
    /// </summary>
    //public class DbPartialWriter : IDbPartialWriter
    //{
    //    private readonly long mMaxPartLength;
    //    private IDataContext mContext;
    //    private FileContent mFileContent;
    //    private FileContentPart mCurrentPart;
    //    private long mCurrentPartPosition;

    //    public DbPartialWriter(IDataContext context, FileContent fileContent)
    //    {
    //        mContext = context;
    //        mFileContent = fileContent;
    //    }

    //    /// <summary>
    //    /// Инициализирует поток данных
    //    /// </summary>
    //    public void Init()
    //    {
    //        var part = mContext.Set<FileContentPart>()
    //            .Where(x => x.ContentId == mFileContent.Id)
    //            .OrderByDescending(x => x.Order)
    //            .FirstOrDefault();

    //        if (part == null)
    //            part = CreateNextPart(null);

    //        mCurrentPart = part;
    //        mCurrentPartPosition = mCurrentPart.Length;
    //        Position = mCurrentPart.Position + mCurrentPart.Length;
    //    }

    //    /// <inheritdoc />
    //    public bool CanSeek => false;

    //    /// <inheritdoc />
    //    public bool HasParts => mCurrentPart != null;

    //    /// <inheritdoc />
    //    public long Position { get; set; }

    //    /// <inheritdoc />
    //    public long TotalLength => mFileContent.CurrentSize;

    //    /// <inheritdoc />
    //    public bool CanWriteToCurrentPart => mCurrentPartPosition < mMaxPartLength;

    //    /// <inheritdoc />
    //    public void SetStreamLength(long value) => throw new NotSupportedException();

    //    /// <inheritdoc />
    //    public void Seek(long streamOffset) => throw new NotSupportedException();

    //    /// <inheritdoc />
    //    public int Write(byte[] buffer, int offset, int count)
    //    {
    //        var writeCount = count;
    //        if (writeCount > mMaxPartLength - mCurrentPartPosition)
    //            writeCount = (int)(mMaxPartLength - mCurrentPartPosition);

    //        //Вычисляем длину нового массива
    //        var newLength = mCurrentPartPosition + writeCount;
    //        var data = mCurrentPart.Data.Bytes;

    //        //Увеличиваем массив если требуется
    //        if (newLength > data.Length)
    //        {
    //            var sizeIncrease = newLength - data.Length;
    //            if (!CanWriteBytes(sizeIncrease))
    //                throw new InvalidOperationException($"Не удается записать {sizeIncrease} байт.");

    //            Array.Resize(ref data, (int)newLength);
    //            UpdatePartData(mCurrentPart, data);
    //            IncreaseSize(sizeIncrease);
    //        }


    //        Array.Copy(buffer, offset, data, mCurrentPartPosition, writeCount);

    //        //update length
    //        mCurrentPartPosition += writeCount;
    //        Position += writeCount;
    //        return writeCount;
    //    }

    //    /// <inheritdoc />
    //    public bool Next()
    //    {
    //        if (mCurrentPart == null)
    //            return false;

    //        var nextPart = mContext.Set<FileContentPart>().FirstOrDefault(x => x.ContentId == mFileContent.Id && x.Position == mCurrentPart.Position + 1);
    //        if (nextPart == null && CanWriteBytes())
    //        {
    //            nextPart = CreateNextPart(mCurrentPart);
    //        }
    //        mCurrentPart = nextPart;
    //        mCurrentPartPosition = 0;
    //        Position = mCurrentPart?.Position ?? TotalLength;
    //        return HasParts;
    //    }

    //    /// <inheritdoc />
    //    public void Flush()
    //    {
    //        mContext.SaveChanges();
    //    }

    //    /// <inheritdoc />
    //    public void Dispose()
    //    {
    //        mContext = null;
    //        mFileContent = null;
    //    }

    //    /// <summary>
    //    /// Проверяет возможность записи указанного количества байт
    //    /// </summary>
    //    private bool CanWriteBytes(long length = 1)
    //    {
    //        return mFileContent.CurrentSize + length <= mFileContent.MaxSize;
    //    }

    //    /// <summary>
    //    /// Обновляет информацию в сущности <see cref="FileContent"/> о том что размер данных увеличился
    //    /// </summary>
    //    private void IncreaseSize(long length)
    //    {
    //        mFileContent.CurrentSize += length;
    //        mFileContent.ChangedDate = DateTime.UtcNow;
    //    }

    //    /// <summary>
    //    /// Обновляет информацию у <see cref="FileContentPart"/>
    //    /// </summary>
    //    private void UpdatePartData(FileContentPart part, byte[] data)
    //    {
    //        part.Data.Bytes = data;
    //        part.Length = mCurrentPart.Data.Bytes.LongLength;
    //    }

    //    /// <summary>
    //    /// Создает новую <see cref="FileContentPart"/> на основе предыдущей
    //    /// </summary>
    //    private FileContentPart CreateNextPart(FileContentPart previous)
    //    {
    //        var part = new FileContentPart()
    //        {
    //            ContentId = mFileContent.Id,
    //            Data = new FileContentPartData
    //            {
    //                Bytes = new byte[0]
    //            },
    //            Length = 0,
    //            Order = previous != null ? previous.Order + 1 : 1,
    //            Position = previous != null ? previous.Position + previous.Length : 0,
    //        };
    //        mContext.Set<FileContentPart>().Add(part);
    //        return part;
    //    }
    //}
}
