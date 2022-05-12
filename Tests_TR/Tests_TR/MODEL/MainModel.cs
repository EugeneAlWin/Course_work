﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Configuration;
#nullable disable
namespace Tests_TR.MODEL
{
    public class MainModel
    {
    }

    public class DatabaseContext : DbContext
    {

        public DbSet<Test> Tests { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseContext()
        {
            // Database.EnsureDeleted();

            Database.EnsureCreated();
            Load_Data();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(User_Builder);
            modelBuilder.Entity<Test>(Test_Builder);
            modelBuilder.Entity<Questions>(Questions_Builder);

        }
        private void User_Builder(EntityTypeBuilder<User> e)
        {
            e.HasKey(i => i.Id).HasName("PK_User");
            e.Property(i => i.Id).ValueGeneratedOnAdd();
            e.HasIndex(i => i.Login).IsUnique();
            e.Property(l => l.Login).HasMaxLength(30).IsRequired();
            e.HasIndex(i => i.Password).IsUnique();
            e.Property(p => p.Password).HasMaxLength(30).IsRequired();
            e.Property(r => r.Role).HasMaxLength(6).IsRequired();
            e.Property(n => n.Name).HasMaxLength(30).IsRequired();
            e.Property(ln => ln.Last_Name).HasMaxLength(30).IsRequired();
            e.Property(fn => fn.Father_Name).HasMaxLength(30);
            e.HasCheckConstraint("CK_User_Role", "role = 'admin' or role = 'user'");
        }
        private void Test_Builder(EntityTypeBuilder<Test> entity)
        {
            entity.HasKey(i => i.Id).HasName("PK_Test");
            entity.Property(i => i.Id).ValueGeneratedNever();
            entity.HasIndex(i => i.Id).IsUnique();
            entity.Property(n => n.Name).HasMaxLength(50).IsRequired();
            entity.Property(t => t.Topic).IsRequired();
            entity.HasMany(t => t.Questions).WithOne(q => q.Test)
                .HasForeignKey(q => q.Test_Id)
                .HasConstraintName("FK_Test_Questions_Test_Id ")
                .OnDelete(DeleteBehavior.Cascade);
        }
        private void Questions_Builder(EntityTypeBuilder<Questions> entity)
        {
            entity.HasKey(i => i.Id).HasName("PK_Questions");
            entity.Property(q => q.Question).HasMaxLength(300).IsRequired();
            entity.Property(a => a.Answer_1).HasMaxLength(200).IsRequired();
            entity.Property(a => a.Answer_2).HasMaxLength(200).IsRequired();
            entity.Property(a => a.Answer_3).HasMaxLength(200);
            entity.Property(a => a.Answer_4).HasMaxLength(200);
            entity.Property(ra => ra.Right_Answer).HasMaxLength(1).IsRequired();
            entity.Property(p => p.Paragraph).HasMaxLength(60);
            entity.Property(i => i.Image).HasMaxLength(150);
            entity.Property(t => t.Test_Id).IsRequired();

        }
        public void Load_Data()
        {
            Users.Load();
            Questions.Load();
            Tests.Load();
        }
    }

    public class Test
    {
        public int Id { get; set; }
        public int Topic { get; set; }
        public string Name { get; set; }
        public List<Questions> Questions { get; set; } = new();

    }

    public class Questions
    {
        public int Id { get; set; }
        public int Test_Id { get; set; }
        public string Question { get; set; }
        public string Answer_1 { get; set; }
        public string Answer_2 { get; set; }
        public string Answer_3 { get; set; }
        public string Answer_4 { get; set; }
        public string Right_Answer { get; set; }
        public string Paragraph { get; set; }
        public string Image { get; set; }
        public Test Test { get; set; }

        public override string ToString()
        {
            return $"{Id}-{Question}-{Answer_1}-{Answer_2}-{Answer_3}-{Answer_4}-{Right_Answer}-{Image}";
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public string Father_Name { get; set; }
        public override string ToString()
        {
            return $"{Id}-{Login}-{Password}-{Role}";
        }
    }

}
