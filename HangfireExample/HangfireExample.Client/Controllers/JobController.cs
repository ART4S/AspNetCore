using Hangfire;
using HangfireExample.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HangfireExample.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IBackgroundJobClient _client;
        private readonly IVideoService _videoService;
        private readonly IEmailService _emailService;

        public JobController(
            IBackgroundJobClient client, 
            IVideoService videoService, 
            IEmailService emailService)
        {
            _client = client;
            _videoService = videoService;
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult Post()
        {
            _client.Enqueue(() => _emailService.Send());
            _client.Enqueue(() => _videoService.Play());

            return Ok();
        }
    }
}
