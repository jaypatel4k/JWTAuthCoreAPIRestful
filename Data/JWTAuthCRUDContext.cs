using JWTAuthCoreAPIRestful.Models;
using JWTAuthCoreAPIRestful.Models.StudentResultModel;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace JWTAuthCoreAPIRestful.Data
{
    public class JWTAuthCRUDContext : DbContext
    {
        public JWTAuthCRUDContext(DbContextOptions options):base(options) { }

        public DbSet<UserModel> Users { get; set; } 
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        //Added for Student rank report
        public DbSet<Division> Division { get; set; }
        public DbSet<Month> Month { get; set; }
        public DbSet<Standard> Standard { get; set; }
        public DbSet<StreamType> StreamType { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentMark> StudentMark { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<TestDuration> TestDuration { get; set; }
        public DbSet<TestHeldOfMark> TestHeldOfMark { get; set; }
        public DbSet<TestType> TestType { get; set; }
        public DbSet<Year> Year { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Student>()
        //        .HasOne<Standard>()
        //        .WithOne()
        //        .HasForeignKey<Student>(s => s.StandId);

        //    modelBuilder.Entity<Student>()
        //        .HasOne<Division>()
        //        .WithOne()
        //        .HasForeignKey<Student>(s => s.DivisionId);
        //}

    }
}
