using FileStoringSample.Data.Context;
using FileStoringSample.Data.Model.Entities;
using FileStoringSample.Data.Repositories.Interfaces;
using FileStoringSample.Web.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using File = FileStoringSample.Data.Model.Entities.File;

namespace FileStoringSample.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IRepo<File, IDataContext> _fileRepo;
        private readonly IFileContentRepo<IDataContext> _fileContentRepo;

        public FileController(
            IRepo<File, IDataContext> fileRepo, 
            IFileContentRepo<IDataContext> fileContentRepo)
        {
            _fileRepo = fileRepo;
            _fileContentRepo = fileContentRepo;
        }

        [HttpGet("{id:guid}/[Action]")]
        public IActionResult Download([FromRoute] Guid id)
        {
            var file = _fileRepo.GetById(id);
            if (file == null)
                return NotFound();

            return Ok(new FileReadDto()
            {
                Data = _fileContentRepo.Read(file.ContentId),
                DataType = $"application/{Path.GetExtension(file.Name).Replace(".", "")}",
                Name = file.Name
            });

        }

        [HttpGet("{id:guid}/[Action]")]
        public IActionResult Upload([FromRoute] Guid id, [FromForm] IFormFile file)
        {
            var newFile = new File()
            {
                Name = file.Name,
                CreationDate = DateTime.UtcNow,
                Size = file.Length,
                Content = new FileContent()
            };

            _fileRepo.Add(newFile);

            using (var stream = _fileContentRepo.Write(newFile.Content.Id))
            {
                file.CopyTo(stream);
            }

            return Ok();
        }
    }
}
