using Microsoft.EntityFrameworkCore;
using Webrox.EntityFrameworkCore.Core;
using System.Threading.Tasks;
using System.Linq;
using Xunit;

namespace Webrox.EntityFrameworkCore.Tests.Shared
{
    public class UnitTest
    {
        internal DbContextOptions<SampleDbContext> _options;

        [Fact]
        public async Task TestRowNumber()
        {
            using var context = new SampleDbContext(_options);

            //var count = await context.Users.CountAsync();
            //Assert.Equal(10, count);

            //var windowFunctions = await context.Users
            //    .Select(a => new
            //    {
            //        Id = a.Id,
            //        RowNumber = EF.Functions.RowNumber(EF.Functions.OrderBy(a.Id)),
            //        RowNumberPartition = EF.Functions.RowNumber(
            //                                EF.Functions.PartitionBy(a.RoleId),
            //                                EF.Functions.OrderBy(a.Id)),
            //        Rank = EF.Functions.Rank(EF.Functions.OrderBy(a.Id)),
            //        RankPartition = EF.Functions.Rank(
            //                                EF.Functions.PartitionBy(a.RoleId),
            //                                EF.Functions.OrderBy(a.Id)),
            //        DenseRank = EF.Functions.DenseRank(EF.Functions.OrderBy(a.Id)),
            //        DenseRankPartition = EF.Functions.DenseRank(
            //                                EF.Functions.PartitionBy(a.RoleId),
            //                                EF.Functions.OrderBy(a.Id)),
            //        Average = EF.Functions.Average(a.Id, EF.Functions.OrderBy(a.Id)),
            //        AveragePartition = EF.Functions.Average(a.Id,
            //                                EF.Functions.PartitionBy(a.RoleId),
            //                                EF.Functions.OrderBy(a.Id)),
            //        Sum = EF.Functions.Sum(a.Id, EF.Functions.OrderBy(a.Id)),
            //        SumPartition = EF.Functions.Sum(a.Id,
            //                                EF.Functions.PartitionBy(a.RoleId),
            //                                EF.Functions.OrderBy(a.Id)),
            //        Min = EF.Functions.Min(a.Id, EF.Functions.OrderBy(a.Id)),
            //        MinPartition = EF.Functions.Min(a.Id,
            //                                EF.Functions.PartitionBy(a.RoleId),
            //                                EF.Functions.OrderBy(a.Id)),

            //        Max = EF.Functions.Max(a.Id, EF.Functions.OrderBy(a.Id)),
            //        MaxPartition = EF.Functions.Max(a.Id,
            //                                EF.Functions.PartitionBy(a.RoleId),
            //                                EF.Functions.OrderBy(a.Id)),


            //    }).ToListAsync();

            //Assert.NotNull(windowFunctions);
            //Assert.Equal(10, windowFunctions.Count);

        }

        [Fact]
        public async Task TestSelect()
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

        [Fact]
        public async Task TestCasts()
        {
            using var context = new SampleDbContext(_options);

            //var count = await context.Users.CountAsync();
            //Assert.Equal(10, count);

            var windowFunctions = await context.Users
                .Select(a => new
                {
                    Id = a.Id,
                    
                    Average_8 = EF.Functions.Average(a.SubRoleId8, EF.Functions.OrderBy(a.Id)),
                    Average_u8 = EF.Functions.Average(a.SubRoleIdu8, EF.Functions.OrderBy(a.Id)),
                    Average_16 = EF.Functions.Average(a.SubRoleId16, EF.Functions.OrderBy(a.Id)),
                    Average_u16 = EF.Functions.Average(a.SubRoleIdu16, EF.Functions.OrderBy(a.Id)),
                    Average_32 = EF.Functions.Average(a.SubRoleId32, EF.Functions.OrderBy(a.Id)),
                    Average_u32 = EF.Functions.Average(a.SubRoleIdu32, EF.Functions.OrderBy(a.Id)),
                    Average_64 = EF.Functions.Average(a.SubRoleId64, EF.Functions.OrderBy(a.Id)),
                    Average_u64 = EF.Functions.Average(a.SubRoleIdu64, EF.Functions.OrderBy(a.Id)),
                    Average_dec = EF.Functions.Average(a.SubRoleIdDecimal, EF.Functions.OrderBy(a.Id)),

                    Sum_8 = EF.Functions.Sum(a.SubRoleId8, EF.Functions.OrderBy(a.Id)),
                    Sum_u8 = EF.Functions.Sum(a.SubRoleIdu8, EF.Functions.OrderBy(a.Id)),
                    Sum_16 = EF.Functions.Sum(a.SubRoleId16, EF.Functions.OrderBy(a.Id)),
                    Sum_u16 = EF.Functions.Sum(a.SubRoleIdu16, EF.Functions.OrderBy(a.Id)),
                    Sum_32 = EF.Functions.Sum(a.SubRoleId32, EF.Functions.OrderBy(a.Id)),
                    Sum_u32 = EF.Functions.Sum(a.SubRoleIdu32, EF.Functions.OrderBy(a.Id)),
                    Sum_64 = EF.Functions.Sum(a.SubRoleId64, EF.Functions.OrderBy(a.Id)),
                    Sum_u64 = EF.Functions.Sum(a.SubRoleIdu64, EF.Functions.OrderBy(a.Id)),
                    Sum_dec = EF.Functions.Sum(a.SubRoleIdDecimal, EF.Functions.OrderBy(a.Id)),

                    Min_8 = EF.Functions.Min(a.SubRoleId8, EF.Functions.OrderBy(a.Id)),
                    Min_u8 = EF.Functions.Min(a.SubRoleIdu8, EF.Functions.OrderBy(a.Id)),
                    Min_16 = EF.Functions.Min(a.SubRoleId16, EF.Functions.OrderBy(a.Id)),
                    Min_u16 = EF.Functions.Min(a.SubRoleIdu16, EF.Functions.OrderBy(a.Id)),
                    Min_32 = EF.Functions.Min(a.SubRoleId32, EF.Functions.OrderBy(a.Id)),
                    Min_u32 = EF.Functions.Min(a.SubRoleIdu32, EF.Functions.OrderBy(a.Id)),
                    Min_64 = EF.Functions.Min(a.SubRoleId64, EF.Functions.OrderBy(a.Id)),
                    Min_u64 = EF.Functions.Min(a.SubRoleIdu64, EF.Functions.OrderBy(a.Id)),
                    Min_dec = EF.Functions.Min(a.SubRoleIdDecimal, EF.Functions.OrderBy(a.Id)),

                    Max_8 = EF.Functions.Max(a.SubRoleId8, EF.Functions.OrderBy(a.Id)),
                    Max_u8 = EF.Functions.Max(a.SubRoleIdu8, EF.Functions.OrderBy(a.Id)),
                    Max_16 = EF.Functions.Max(a.SubRoleId16, EF.Functions.OrderBy(a.Id)),
                    Max_u16 = EF.Functions.Max(a.SubRoleIdu16, EF.Functions.OrderBy(a.Id)),
                    Max_32 = EF.Functions.Max(a.SubRoleId32, EF.Functions.OrderBy(a.Id)),
                    Max_u32 = EF.Functions.Max(a.SubRoleIdu32, EF.Functions.OrderBy(a.Id)),
                    Max_64 = EF.Functions.Max(a.SubRoleId64, EF.Functions.OrderBy(a.Id)),
                    Max_u64 = EF.Functions.Max(a.SubRoleIdu64, EF.Functions.OrderBy(a.Id)),
                    Max_dec = EF.Functions.Max(a.SubRoleIdDecimal, EF.Functions.OrderBy(a.Id)),

                    // missing NTile

                }).ToListAsync();

            Assert.NotNull(windowFunctions);
            Assert.Equal(10, windowFunctions.Count);

        }
    }
}