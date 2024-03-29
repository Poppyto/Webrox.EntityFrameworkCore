﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Webrox.EntityFrameworkCore.Core.Infrastructure;
using Webrox.EntityFrameworkCore.Core.SqlExpressions;

namespace Webrox.EntityFrameworkCore.SqlServer.Query
{
    /// <inheritdoc />
    [SuppressMessage("Usage", "EF1001", MessageId = "Internal EF Core API usage.")]
    public class WebroxSqlServerQuerySqlGenerator : SqlServerQuerySqlGenerator
    {
        //private readonly ITenantDatabaseProvider _databaseProvider;
        private readonly WebroxQuerySqlGenerator _webroxQuerySqlGenerator;
        /// <inheritdoc />
        public WebroxSqlServerQuerySqlGenerator(
           QuerySqlGeneratorDependencies dependencies,
           IRelationalTypeMappingSource typeMappingSource,
#if NET8_0_OR_GREATER
           ISqlServerSingletonOptions sqlServerSingletonOptions,
#endif
           WebroxQuerySqlGenerator webroxQuerySqlGenerator
            //ITenantDatabaseProvider databaseProvider
            )
#if NET8_0_OR_GREATER
           : base(dependencies, typeMappingSource, sqlServerSingletonOptions)
#else
            : base(dependencies
#if NET7_0_OR_GREATER
                  , typeMappingSource
#endif
                  )
#endif
        {
            _webroxQuerySqlGenerator = webroxQuerySqlGenerator;
           // _databaseProvider = databaseProvider ?? throw new ArgumentNullException(nameof(databaseProvider));
        }
        protected override Expression VisitSelect(SelectExpression selectExpression)
        {
            //bool hasWindowFunction = selectExpression.Projection.Cast<ProjectionExpression>().Any(a => a.Expression is RowNumberExpression || a.Expression is WindowExpression);
            //if (hasWindowFunction)
            //{
            //    selectExpression.PushdownIntoSubquery();// this.Visit(selectExpression);
            //}

            return base.VisitSelect(selectExpression);
        }

        /// <inheritdoc />
        protected override Expression VisitExtension(Expression extensionExpression)
        {
            switch (extensionExpression)
            {
                case WindowExpression windowExpression:
                    return _webroxQuerySqlGenerator.VisitWindowFunction(Sql, windowExpression, Visit, VisitOrdering);
                default:
                    return base.VisitExtension(extensionExpression);
            }
        }
    }
}
