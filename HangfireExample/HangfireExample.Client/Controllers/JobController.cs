using Hangfire;
using HangfireExample.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using Hangfire.Common;
using Hangfire.States;

namespace HangfireExample.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IBackgroundJobClient _jobClient;
        private readonly IRecurringJobManager _recurringJobManager;
        private readonly IEmailService _emailService;

        public JobController(
            IBackgroundJobClient jobClient, 
            IEmailService emailService, 
            IRecurringJobManager recurringJobManager)
        {
            _jobClient = jobClient;
            _recurringJobManager = recurringJobManager;
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult Post()
        {
            Expression<Action> job = () => _emailService.Send();

            //var jobId = _jobClient.Enqueue(job);

            //var jobId = _jobClient.Create(Job.FromExpression(job), new EnqueuedState("default"));

            //var parentId = _jobClient.Schedule(job, TimeSpan.FromMinutes(1));
            //_jobClient.ContinueJobWith(parentId, job, JobContinuationOptions.OnlyOnSucceededState);

            //_recurringJobManager.AddOrUpdate(
            //    recurringJobId: Guid.NewGuid().ToString(),
            //    methodCall: job,
            //    cronExpression: Cron.Minutely,
            //    timeZone: TimeZoneInfo.Utc,
            //    queue: "recurring");

            return Ok();
        }
    }
}
