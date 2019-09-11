﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using WebFeatures.Application.Infrastructure.Results;
using WebFeatures.Application.Pipeline.Mediators;

namespace WebFeatures.WebApi.Controllers.Base
{
    /// <summary>
    /// Базовый класс для контроллеров
    /// </summary>
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// Посредник для отправки запроса подходящим обработчикам
        /// </summary>
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        private IMediator _mediator;

        /// <summary>
        /// Ответ с результатом
        /// </summary>
        /// <typeparam name="TSuccess">Тип успешного результата</typeparam>
        /// <typeparam name="TFailure">Тип результата с ошибкой</typeparam>
        /// <param name="result">Результат выполнения запроса/команды</param>
        protected IActionResult ResultResponse<TSuccess, TFailure>(Result<TSuccess, TFailure> result)
        {
            if (result.IsSuccess && !EqualityComparer<TSuccess>.Default.Equals(result.SuccessValue, default))
                return StatusCode(result.StatusCode, result.SuccessValue);

            if (!result.IsSuccess && !EqualityComparer<TFailure>.Default.Equals(result.FailureValue, default))
                return StatusCode(result.StatusCode, result.FailureValue);

            return StatusCode(result.StatusCode);
        }

        /// <summary>
        /// Ответ с результатом
        /// </summary>
        /// <param name="result">Результат выполнения команды</param>
        protected IActionResult ResultResponse(Result result)
        {
            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, result.FailureValue);

            return StatusCode(result.StatusCode);
        }
    }
}
