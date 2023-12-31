﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;
using Webrox.EntityFrameworkCore.Core.Infrastructure;

namespace Webrox.EntityFrameworkCore.Sqlite.Query
{
    /// <inheritdoc />
    [SuppressMessage("Usage", "EF1001:Internal EF Core API usage.")]
    public class WebroxSqliteQuerySqlGeneratorFactory : IQuerySqlGeneratorFactory
    {
        private readonly QuerySqlGeneratorDependencies _dependencies;
        private readonly IRelationalTypeMappingSource _typeMappingSource;
        // private readonly ISqlServerSingletonOptions _sqlServerSingletonOptions;
        // private readonly ITenantDatabaseProviderFactory _databaseProviderFactory;
        private readonly WebroxQuerySqlGenerator _webroxQuerySqlGenerator;

        /// <summary>
        /// Initializes new instance of <see cref="WebroxSqliteQuerySqlGeneratorFactory"/>.
        /// </summary>
        /// <param name="dependencies">Dependencies.</param>
        /// <param name="typeMappingSource">Type mapping source.</param>
        /// <param name="sqlServerSingletonOptions">Options.</param>
        /// <param name="databaseProviderFactory">Factory.</param>
        public WebroxSqliteQuerySqlGeneratorFactory(
           QuerySqlGeneratorDependencies dependencies,
           IRelationalTypeMappingSource typeMappingSource,
            WebroxQuerySqlGenerator webroxQuerySqlGenerator
           //ISqlServerSingletonOptions sqlServerSingletonOptions,
           //ITenantDatabaseProviderFactory databaseProviderFactory
           )
        {
            _dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
            _typeMappingSource = typeMappingSource ?? throw new ArgumentNullException(nameof(typeMappingSource));
            _webroxQuerySqlGenerator = webroxQuerySqlGenerator;
            //_sqlServerSingletonOptions = sqlServerSingletonOptions;
            //_databaseProviderFactory = databaseProviderFactory ?? throw new ArgumentNullException(nameof(databaseProviderFactory));
        }

        /// <inheritdoc />
        public QuerySqlGenerator Create()
        {
            return new WebroxSqliteQuerySqlGenerator(_dependencies, _typeMappingSource, _webroxQuerySqlGenerator);//, _sqlServerSingletonOptions, _databaseProviderFactory.Create());
        }
    }
}
