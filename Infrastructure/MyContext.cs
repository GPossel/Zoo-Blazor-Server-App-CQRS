using Infrastructure.Entities.Followers;
using Infrastructure.Entities.Users;
using Infrastructure.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class MyContext : DbContext
    {
        public DbSet<NameDbo> Names { get; set; }
        public DbSet<EmailDbo> Emails { get; set; }
        public DbSet<UserDbo> Users { get; set; }
        public DbSet<FollowerDbo> Followers { get; set; }

        public MyContext(DbContextOptions opt) : base(opt)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-6ME9173N\MSSQLSERVER01;Database=ZooDatabase;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NameDbo>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(e => e.User)
                      .WithOne(e => e.Name)
                      .HasForeignKey<NameDbo>(e => e.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EmailDbo>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(e => e.User)
                      .WithOne(e => e.Email)
                      .HasForeignKey<EmailDbo>(e => e.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasAlternateKey(e => e.EmailAddress);
            });

            modelBuilder.Entity<UserDbo>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.HasOne(e => e.Email)
                      .WithOne(e => e.User)
                      .HasForeignKey<EmailDbo>(e => e.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Name)
                      .WithOne(e => e.User)
                      .HasForeignKey<NameDbo>(e => e.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FollowerDbo>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(e => e.User)
                      .WithMany(e => e.Followers)
                      .HasForeignKey(e => e.UserId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasAlternateKey(e => new { e.UserId, e.FollowedId, e.FollowedAt });
            });


        }

    }
}
