﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Web.Abstractions;
using Web.Extensions;
using Web.Infrastructure;

namespace Web.Features.Registration
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class RegistrationController : Controller
    {
        [HttpPost]
        public IActionResult RegisterUser(
            [FromBody, Required] RegisterUser command, 
            [FromServices] IHandler<RegisterUser, Result> commandHandler)
        {
            var result = commandHandler.Handle(command);
            return this.ResultResponse(result);
        }
    }
}