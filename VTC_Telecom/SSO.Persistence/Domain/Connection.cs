using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SSO.Persistence.Domain
{
    public partial class Connection : DbContext
    {
        public Connection()
            : base("name=Connection")
        {
        }

        public virtual DbSet<VTC_Apps> VTC_Apps { get; set; }
        public virtual DbSet<VTC_GroupRole> VTC_GroupRole { get; set; }
        public virtual DbSet<VTC_Groups> VTC_Groups { get; set; }
        public virtual DbSet<VTC_GroupUser> VTC_GroupUser { get; set; }
        public virtual DbSet<VTC_Roles> VTC_Roles { get; set; }
        public virtual DbSet<VTC_Token> VTC_Token { get; set; }
        public virtual DbSet<VTC_Users> VTC_Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VTC_Apps>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Apps>()
                .Property(e => e.AppId)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Apps>()
                .Property(e => e.Domain)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Apps>()
                .Property(e => e.SecretKey)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Apps>()
                .HasMany(e => e.VTC_Roles)
                .WithOptional(e => e.VTC_Apps)
                .HasForeignKey(e => e.AppId);

            modelBuilder.Entity<VTC_Apps>()
                .HasMany(e => e.VTC_Token)
                .WithOptional(e => e.VTC_Apps)
                .HasForeignKey(e => e.AppId);

            modelBuilder.Entity<VTC_GroupRole>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_GroupRole>()
                .Property(e => e.GroupId)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_GroupRole>()
                .Property(e => e.RoleId)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Groups>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Groups>()
                .HasMany(e => e.VTC_GroupRole)
                .WithRequired(e => e.VTC_Groups)
                .HasForeignKey(e => e.GroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VTC_Groups>()
                .HasMany(e => e.VTC_GroupUser)
                .WithRequired(e => e.VTC_Groups)
                .HasForeignKey(e => e.GroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VTC_GroupUser>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_GroupUser>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_GroupUser>()
                .Property(e => e.GroupId)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Roles>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Roles>()
                .Property(e => e.AppId)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Roles>()
                .Property(e => e.Permission)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Roles>()
                .HasMany(e => e.VTC_GroupRole)
                .WithRequired(e => e.VTC_Roles)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VTC_Token>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Token>()
                .Property(e => e.UserId)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Token>()
                .Property(e => e.Token)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Token>()
                .Property(e => e.AppId)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Users>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Users>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Users>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Users>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Users>()
                .Property(e => e.Phone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VTC_Users>()
                .HasMany(e => e.VTC_GroupUser)
                .WithRequired(e => e.VTC_Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VTC_Users>()
                .HasMany(e => e.VTC_Token)
                .WithOptional(e => e.VTC_Users)
                .HasForeignKey(e => e.UserId);
        }
    }
}
