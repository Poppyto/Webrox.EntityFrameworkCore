﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace Webrox.EntityFrameworkCore.Core.Infrastructure
{
    /// <summary>
    /// Webrox DbContextOptionsExtension
    /// </summary>
    public class WebroxDbContextOptionsExtension : IDbContextOptionsExtension
    {
        WebroxDbContextOptionsExtensionInfo _info;
        /// <summary>
        /// Row Number Support
        /// </summary>
        public bool AddWebroxFeatures { get; set; }

        /// <inheritdoc/>
        public DbContextOptionsExtensionInfo Info => _info;

        /// <summary>
        /// Constructor
        /// </summary>
        public WebroxDbContextOptionsExtension()
        {
            _info = new(this);
        }

        /// <inheritdoc/>
        public void ApplyServices(IServiceCollection services)
        {
            services.AddScoped<WebroxQuerySqlGenerator>();
            services.AddScoped<IMethodCallTranslatorPlugin, WebroxMethodCallTranslatorPlugin>();
        }

        /// <inheritdoc/>
        public void Validate(IDbContextOptions options)
        {
        }
    }
}