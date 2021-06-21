using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace student_webapi.Models
{
    public class StudentApiContext : DbContext
    {
        public DbSet<StudentItem> Students { get; set; }
        public DbSet<GradeItem> Grades { get; set; }
        public StudentApiContext(DbContextOptions<StudentApiContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<StudentItem>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<StudentItem>()
                .Property(t => t.Name)
                .IsRequired(required: true).IsUnicode(unicode: true);

            modelBuilder.Entity<StudentItem>()
                .HasData(new[] 
                {
                    new StudentItem { Id = 1, Name = "Gipsz Jakab", Year = 5, Born = new DateTime(2000, 1, 1), PhoneNumber = "100-2030" },
                    new StudentItem { Id = 2, Name = "Gipsz Jolán", Year = 3, Born = new DateTime(2002, 11, 25), PhoneNumber = "101-2131" },
                    new StudentItem { Id = 3, Name = "Gipsz Judit", Year = 3, Born = new DateTime(2004, 4, 2), PhoneNumber = "102-2232" },
                    new StudentItem { Id = 4, Name = "Gipsz Jóska", Year = 2, Born = new DateTime(2005, 7, 15), PhoneNumber = "103-2333" }
                });

            modelBuilder.Entity<GradeItem>()
                .HasKey(g => g.Id);

            modelBuilder.Entity<GradeItem>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId)
                ;

            modelBuilder.Entity<GradeItem>()
                .HasData(new[]
                {
                    new GradeItem { Id = 1, Grade = 4, StudentId = 1 },
                    new GradeItem { Id = 2, Grade = 3, StudentId = 2 },
                    new GradeItem { Id = 3, Grade = 4, StudentId = 3 },
                    new GradeItem { Id = 4, Grade = 5, StudentId = 2 },
                    new GradeItem { Id = 5, Grade = 1, StudentId = 1 }
                });
        }
    }
}
