﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Webrox.EntityFrameworkCore.Sqlite.Query;

namespace Webrox.EntityFrameworkCore.Sqlite
{
    /// <summary>
    /// DbContextOptionsBuilder Extensions
    /// </summary>
    public static class DbContextOptionsBuilderExtensions
    {
        /// <summary>
        /// Add RowNumber support
        /// </summary>
        /// <param name="optionsBuilder">options Builder</param>
        /// <returns><see cref="SqliteDbContextOptionsBuilder"/></returns>
        public static SqliteDbContextOptionsBuilder AddRowNumberSupport(
                   this SqliteDbContextOptionsBuilder optionsBuilder)
        {
            var infrastructure = (IRelationalDbContextOptionsBuilderInfrastructure)optionsBuilder;

            Core.WebroxDbContextOptionsBuilderExtensions.AddRowNumberSupport(infrastructure);

            infrastructure.OptionsBuilder.ReplaceService<IRelationalParameterBasedSqlProcessorFactory, WebroxSqliteParameterBasedSqlProcessorFactory>();
            infrastructure.OptionsBuilder.ReplaceService<IQuerySqlGeneratorFactory, WebroxSqliteQuerySqlGeneratorFactory>();


            return optionsBuilder;
        }
    }
}