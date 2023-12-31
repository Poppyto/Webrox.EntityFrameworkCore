﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace Webrox.EntityFrameworkCore.Tests.Shared
{
    internal class SampleDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SampleDbContext()
        {
        }

        public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(opt =>
            {
                opt.HasKey(e => e.Id);

                if (this.Database.ProviderName.Contains("MySQL", System.StringComparison.OrdinalIgnoreCase))
                {
                    opt.Property(e => e.SubRoleId8).HasColumnType("tinyint(3)");
                    opt.Property(e => e.SubRoleId16).HasColumnType("smallint");
                    opt.Property(e => e.SubRoleIdu16).HasColumnType("smallint unsigned");
                    opt.Property(e => e.SubRoleIdu32).HasColumnType("int unsigned");
                    opt.Property(e => e.SubRoleIdu64).HasColumnType("bigint unsigned");
                }
            });


            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, RoleId = 1, SubRoleId = 3, SubRoleId8 = (sbyte)0, Email = "sample1@gm.com" },
                new User { Id = 2, RoleId = 3, SubRoleId = 3, SubRoleId8 = (sbyte)0, Email = "sample2@gm.com" },
                new User { Id = 3, RoleId = 2, SubRoleId = 2, SubRoleId8 = (sbyte)0, Email = "sample3@gm.com" },
                new User { Id = 5, RoleId = 2, SubRoleId = 2, SubRoleId8 = (sbyte)0, Email = "sample5@gm.com" },
                new User { Id = 6, RoleId = 1, SubRoleId = 1, Email = "sample6@gm.com" },
                new User { Id = 7, RoleId = 2, SubRoleId = 2, Email = "sample7@gm.com" },
                new User { Id = 8, RoleId = 3, SubRoleId = 3, Email = "sample8@gm.com" },
                new User { Id = 20, RoleId = 1, SubRoleId = 1, Email = "sample20@gm.com" },
                new User { Id = 21, RoleId = 3, SubRoleId = 3, Email = "sample21@gm.com" },
                new User { Id = 30, RoleId = 1, SubRoleId = 1, Email = "sample30@gm.com" }

                );
        }
    }

    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public byte SubRoleId { get; set; }
        public sbyte SubRoleId8 { get; set; }
        public byte SubRoleIdu8 { get; set; }
        public short SubRoleId16 { get; set; }
        public ushort SubRoleIdu16 { get; set; }
        public int SubRoleId32 { get; set; }
        public uint SubRoleIdu32 { get; set; }
        public long SubRoleId64 { get; set; }
        public ulong SubRoleIdu64 { get; set; }
        public decimal SubRoleIdDecimal { get; set; }
        public float SubRoleIdFloat{ get; set; }
        public double SubRoleIdDouble{ get; set; }

        public string Email { get; set; }
    }
}
