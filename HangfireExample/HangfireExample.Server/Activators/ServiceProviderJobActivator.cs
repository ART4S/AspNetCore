﻿using System;
using Hangfire;

namespace HangfireExample.Server.Activators
{
    public class ServiceProviderJobActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderJobActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type jobType)
        {
            return _serviceProvider.GetService(jobType);
        }
    }
}
