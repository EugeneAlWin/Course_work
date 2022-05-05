using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Windows;
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
            //Database.EnsureDeleted();
            bool temp = Database.EnsureCreatedAsync().Result;
            //MessageBox.Show(temp ? "Бд создана" : "Бд существует");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            }
            catch (Exception)
            {
                MessageBox.Show("Проверьте подключение к серверу.\nВозможно, вам стоит проверить строку подключения в App.config.");
                Application.Current.Shutdown();
            }

        }
    }

    public class Test
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Topic { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        public List<Questions> Questions { get; set; } = new();

    }

    public class Questions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Question { get; set; }
        [Required]
        [MaxLength(150)]
        public string Answer_1 { get; set; }
        [Required]
        [MaxLength(150)]
        public string Answer_2 { get; set; }
        [MaxLength(150)]
        public string? Answer_3 { get; set; }
        [MaxLength(150)]
        public string? Answer_4 { get; set; }
        [MaxLength(150)]
        [Required]
        public string Right_Answer { get; set; }
        [MaxLength(150)]
        public string? Image { get; set; }
        public Test Test { get; set; }

        public override string ToString()
        {
            return $"{Id}-{Question}-{Answer_1}-{Answer_2}-{Answer_3}-{Answer_4}-{Right_Answer}-{Image}";
        }
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Login { get; set; }
        [Required]
        [MaxLength(30)]
        public string Password { get; set; }
        [Required]
        [MaxLength(30)]
        public string Role { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Last_Name { get; set; }
        [MaxLength(30)]
        public string Father_Name { get; set; }
        public override string ToString()
        {
            return $"{Id}-{Login}-{Password}-{Role}";
        }
    }

}
