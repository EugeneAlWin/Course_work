using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

#nullable disable
namespace Tests_TR.MODEL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseContext()
        {
            // Database.EnsureDeleted();

            var temp_Result = Database.EnsureCreated();

            if (temp_Result)
            {
                string sql_Raw = "";
                var taskA = Task.Run(() => sql_Raw = File.ReadAllText(@"C:\Users\evgen\Desktop\Курсач ООП\Tests_TR\Tests_TR\VIEW\RESOURCES\Questions.sql"));
                string Server_Name = ConfigurationManager.ConnectionStrings["NameOfDatabase"].ConnectionString;
                taskA.Wait();
                Database.ExecuteSqlRaw(string.Format(sql_Raw, Server_Name));
            }

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
            e.Property(p => p.Password).IsRequired();
            e.Property(r => r.Role).HasMaxLength(6).IsRequired();
            e.Property(n => n.Name).HasMaxLength(30).IsRequired();
            e.Property(ln => ln.Last_Name).HasMaxLength(30).IsRequired();
            e.Property(fn => fn.Father_Name).HasMaxLength(30);
            e.HasCheckConstraint("CK_User_Login", "len(login) > 4");
            e.HasCheckConstraint("CK_User_Password", "len(password) > 4");
            e.HasCheckConstraint("CK_User_Role", "role = 'admin' or role = 'user'");
            e.HasCheckConstraint("CK_User_Name", "len(name) > 0");
            e.HasCheckConstraint("CK_User_Last_Name", "len(last_name) > 0");
        }

        private void Test_Builder(EntityTypeBuilder<Test> e)
        {
            e.HasKey(i => i.Id).HasName("PK_Test");
            e.Property(i => i.Id).ValueGeneratedNever();
            e.HasIndex(i => i.Id).IsUnique();
            e.Property(n => n.Name).HasMaxLength(50).IsRequired();
            e.Property(t => t.Topic).IsRequired();

            e.HasMany(t => t.Questions)
                .WithOne(q => q.Test)
                .HasForeignKey(q => q.Test_Id)
                .HasConstraintName("FK_Test_Questions_Test_Id ")
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void Questions_Builder(EntityTypeBuilder<Questions> e)
        {
            e.HasKey(i => i.Id).HasName("PK_Questions");
            e.Property(q => q.Question).HasMaxLength(300).IsRequired();
            e.Property(a => a.Answer_1).HasMaxLength(200).IsRequired();
            e.Property(a => a.Answer_2).HasMaxLength(200).IsRequired();
            e.Property(a => a.Answer_3).HasMaxLength(200);
            e.Property(a => a.Answer_4).HasMaxLength(200);
            e.Property(ra => ra.Right_Answer).HasMaxLength(1).IsRequired();
            e.Property(p => p.Paragraph).HasMaxLength(60);
            e.Property(i => i.Image).HasMaxLength(150);
            e.Property(t => t.Test_Id).IsRequired();
            e.HasCheckConstraint("CK_Questions_Id", "Test_Id >= 0 and Test_Id <= 7");
            e.HasCheckConstraint("CK_Questions_Question", "len(Question) > 0");
            e.HasCheckConstraint("CK_Questions_Answer_1", "len(Answer_1) > 0");
            e.HasCheckConstraint("CK_Questions_Answer_2", "len(Answer_2) > 0");
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
    }

    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password
        {
            get => password;
            set => password = PagesViewModel.ComputeHash(value);
        }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Last_Name { get; set; }
        public string Father_Name { get; set; }

        private string password;
    }
}