using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Webrox.EntityFrameworkCore.Core;
using Webrox.EntityFrameworkCore.Postgres;
using Xunit;

namespace Webrox.EntityFrameworkCore.Postgres.Tests
{
    public class UnitTestPostgres : IDisposable
    {
        private readonly NpgsqlConnection _connection;
        private readonly DbContextOptions<SampleDbContext> _options;

        public void DeleteDatabase()
        {
            using var connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=Popome690$;Database=efcore;");
            connection.Open();
            var options = new DbContextOptionsBuilder<SampleDbContext>()
                .UseNpgsql(connection)
                .Options;

            using (var context = new SampleDbContext(options))
            {
                context.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS users");//.EnsureDeleted();
            }
        }

        public UnitTestPostgres()
        {
            DeleteDatabase();

            _connection = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=Popome690$;Database=efcore;");
            _connection.Open();

            try
            {
                using (var context = new SampleDbContext(_options))
                {
                    context.Database.EnsureDeleted();
                }
            }
            catch
            {

            }

            _options = new DbContextOptionsBuilder<SampleDbContext>()
                .UseNpgsql(_connection, opt =>
                {
                    opt.AddRowNumberSupport();

                })
                .LogTo(logText =>
                {
                    bool isMySQL = true;
                    var splittedLogText = logText.Split(Environment.NewLine).ToList();
                    splittedLogText[0] = $"{new string('-', 2)}{(isMySQL ? "MySQL" : "SQLServer")}{new string('-', 80)}";
                    splittedLogText.Insert(0, string.Empty);
                    splittedLogText.Insert(2, string.Empty);

                    logText = string.Join(Environment.NewLine, splittedLogText);

                    //logger?.LogTrace(logText);

                    var fgColor = Console.ForegroundColor;
                    Console.ForegroundColor = isMySQL ? ConsoleColor.Blue : ConsoleColor.Yellow;
                    Console.WriteLine(logText);
                    Debug.WriteLine(logText);
                    Console.ForegroundColor = fgColor;
                },
                (b, c) =>
                {
                    return (b.Id == RelationalEventId.CommandExecuting); //only SQL Queries
                })
                .Options;


            using (var context = new SampleDbContext(_options))
            {
                context.Database.EnsureCreated();
            }

        }

        public void Dispose()
        {
            _connection.Close();
        }

        [Fact]
        public async Task TestRowNumber_UsingPostgres()
        {
            using var context = new SampleDbContext(_options);

            var count = await context.Users.CountAsync();
            Assert.Equal(10, count);

            var windowFunctions = await context.Users
                .Select(a => new
                {
                    Id = a.Id,
                    RowNumber = EF.Functions.RowNumber(EF.Functions.OrderBy(a.Id)),
                    RowNumberPartition = EF.Functions.RowNumber(
                                            EF.Functions.PartitionBy(a.RoleId),
                                            EF.Functions.OrderBy(a.Id)),
                    Rank = EF.Functions.Rank(EF.Functions.OrderBy(a.Id)),
                    RankPartition = EF.Functions.Rank(
                                            EF.Functions.PartitionBy(a.RoleId),
                                            EF.Functions.OrderBy(a.Id)),
                    DenseRank = EF.Functions.DenseRank(EF.Functions.OrderBy(a.Id)),
                    DenseRankPartition = EF.Functions.DenseRank(
                                            EF.Functions.PartitionBy(a.RoleId),
                                            EF.Functions.OrderBy(a.Id)),
                    Average = EF.Functions.Average(a.Id, EF.Functions.OrderBy(a.Id)),
                    AveragePartition = EF.Functions.Average(a.Id,
                                            EF.Functions.PartitionBy(a.RoleId),
                                            EF.Functions.OrderBy(a.Id)),
                    Sum = EF.Functions.Sum(a.Id, EF.Functions.OrderBy(a.Id)),
                    SumPartition = EF.Functions.Sum(a.Id,
                                            EF.Functions.PartitionBy(a.RoleId),
                                            EF.Functions.OrderBy(a.Id)),
                    Min = EF.Functions.Min(a.Id, EF.Functions.OrderBy(a.Id)),
                    MinPartition = EF.Functions.Min(a.Id,
                                            EF.Functions.PartitionBy(a.RoleId),
                                            EF.Functions.OrderBy(a.Id)),

                    Max = EF.Functions.Max(a.SubRoleId, EF.Functions.OrderBy(a.Id)),
                    MaxPartition = EF.Functions.Max(a.Id,
                                            EF.Functions.PartitionBy(a.RoleId),
                                            EF.Functions.OrderBy(a.Id)),


                }).ToListAsync();

            Assert.NotNull(windowFunctions);
            Assert.Equal(10, windowFunctions.Count);

        }

        [Fact]
        public async Task TestSelect_UsingPostgres()
        {
            using var context = new SampleDbContext(_options);

            //var count = await context.Users.CountAsync();
            //Assert.Equal(10, count);

            var windowFunctions = await context.Users
                .Select((a, index) => new
                {
                    Id = a.Id,
                    Index = index
                })
                .ToListAsync();

            Assert.NotNull(windowFunctions);
            Assert.Equal(10, windowFunctions.Count);

        }
    }
}