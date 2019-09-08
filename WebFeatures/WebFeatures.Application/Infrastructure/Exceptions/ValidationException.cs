﻿using System;
using WebFeatures.Application.Infrastructure.Failures;

namespace WebFeatures.Application.Infrastructure.Exceptions
{
    public class ValidationException : Exception
    {
        public Fail Fail { get; }

        public ValidationException(Fail fail)
        {
            Fail = fail;
        }
    }
}
