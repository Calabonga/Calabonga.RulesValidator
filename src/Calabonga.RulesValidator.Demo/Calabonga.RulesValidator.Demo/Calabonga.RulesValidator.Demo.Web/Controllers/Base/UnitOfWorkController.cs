﻿using System;
using Calabonga.EntityFrameworkCore.UOW;
using Calabonga.RulesValidator.Demo.Core.Exceptions;
using Calabonga.RulesValidator.Demo.Data;
using Calabonga.RulesValidator.Demo.Models;
using Calabonga.RulesValidatorDemo.Web.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Calabonga.RulesValidatorDemo.Web.Controllers.Base
{
    /// <summary>
    /// Unit Of Work controller
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    public abstract class UnitOfWorkController : OperationResultController
    {
        /// <inheritdoc />
        protected UnitOfWorkController(IOptions<CurrentAppSettings> options,
            IUnitOfWork<ApplicationDbContext> unitOfWork)
        {
            CurrentSettings = options.Value;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        /// Allows to save data logs to the database Logs table
        /// </summary>
        protected void LogInformation(string message, Exception exception = null)
        {
            var logs = UnitOfWork.GetRepository<Log>();
            var log = new Log
            {
                CreatedAt = DateTime.UtcNow,
                Level = LogLevel.Information.ToString(),
                Logger = GetType().Name,
                Message = message,
                ThreadId = "0",
                ExceptionMessage = exception?.Message
            };
            logs.Insert(log);
            UnitOfWork.SaveChanges();
            if (!UnitOfWork.LastSaveChangesResult.IsOk)
            {
                throw new MicroserviceInvalidOperationException();
            }
        }

        /// <summary>
        /// Allows to save data logs to the database Logs table
        /// </summary>
        private void Log(LogLevel level, string message, Exception exception = null)
        {
            var logs = UnitOfWork.GetRepository<Log>();
            var log = new Log
            {
                CreatedAt = DateTime.UtcNow,
                Level = level.ToString(),
                Logger = GetType().Name,
                Message = message,
                ThreadId = "0",
                ExceptionMessage = exception?.Message
            };
            logs.Insert(log);
            UnitOfWork.SaveChanges();
            if (!UnitOfWork.LastSaveChangesResult.IsOk)
            {
                throw new MicroserviceInvalidOperationException();
            }
        }


        /// <summary>
        /// Current Unit Of Work 
        /// </summary>
        protected IUnitOfWork<ApplicationDbContext> UnitOfWork { get; set; }

        /// <summary>
        /// Current application settings
        /// </summary>
        protected CurrentAppSettings CurrentSettings { get; }
    }
}