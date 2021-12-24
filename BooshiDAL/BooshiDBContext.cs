using BooshiDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BooshiDAL
{
     public partial class BooshiDBContext : DbContext
    {

        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserDetails> UsersDetails { get; set; }
        DbSet<Delivery> Deliveries { get; set; }
        DbSet<DeliveryStatus> DeliveryStatuses { get; set; }
        DbSet<Origin> Origins { get; set; }
        DbSet<Destination> Destinations { get; set; }

        public BooshiDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Role>().HasIndex(r => r.RoleName).IsUnique();

            modelBuilder.Entity<Delivery>().Property(d => d.Created).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Delivery>().Property(d => d.Id).UseIdentityColumn(3000, 1);
            modelBuilder.Entity<Origin>().HasOne(x => x.Delivery).WithOne(x => x.Origin).HasForeignKey<Origin>(x => x.DeliveryId).IsRequired();
            modelBuilder.Entity<Destination>().HasOne(x => x.Delivery).WithOne(x => x.Destination).HasForeignKey<Destination>(x => x.DeliveryId).IsRequired();
            base.OnModelCreating(modelBuilder);

        }
    }
}
