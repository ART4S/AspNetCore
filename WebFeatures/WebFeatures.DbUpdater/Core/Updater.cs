﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using AppContext = WebFeatures.DataContext.AppContext;

namespace WebFeatures.DbUpdater.Core
{
    /// <summary>
    /// Обновление БД
    /// </summary>
    class Updater
    {
        private readonly ILogger<Updater> _logger;
        private readonly UpdaterOptions _options;
        private readonly AppContext _context;

        public Updater(ILogger<Updater> logger, IOptions<UpdaterOptions> options, AppContext context)
        {
            _logger = logger;
            _options = options.Value;
            _context = context;
        }

        public void Run()
        {
            try
            {
                if (_options.Recreate && _options.Migrate)
                {
                    _context.Database.EnsureDeleted();
                    _context.Database.Migrate();
                    return;
                }

                if (_options.Migrate)
                {
                    _context.Database.Migrate();
                    return;
                }

                if (_options.Recreate)
                {
                    _context.Database.EnsureDeleted();

                    if (_context.Database.GetPendingMigrations().Any())
                    {
                        _context.Database.Migrate();
                    }
                    else
                    {
                        _context.Database.EnsureCreated();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Неудачная попытка обновления БД");
            }
        }
    }
}
