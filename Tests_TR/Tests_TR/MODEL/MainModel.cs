using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=preCourse2;Trusted_Connection=True;");
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
        public string Right_Answer { get; set; }
        [Required]
        [MaxLength(150)]
        public string Image { get; set; }
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

    }

}
