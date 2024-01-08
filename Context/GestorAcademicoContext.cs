using Gestor_Acadêmico.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Context
{
    public class GestorAcademicoContext : DbContext
    {
        public GestorAcademicoContext(DbContextOptions<GestorAcademicoContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CategoryCourse> CategoriesCourse { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<StudentGrade> StudentGrades { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Turn> Turns { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.Entity<StudentCourse>().HasKey(stucou => new { stucou.StudentId, stucou.CourseId });
            modelBuilder.Entity<StudentCourse>()
                .HasOne(stucou => stucou.Student)
                .WithMany(stu => stu.Courses)
                .HasForeignKey(stucou => stucou.StudentId);
            modelBuilder.Entity<StudentCourse>()
                .HasOne(stucou => stucou.Course)
                .WithMany(cou => cou.Students)
                .HasForeignKey(stucou => stucou.CourseId);


            modelBuilder.Entity<StudentSubject>().HasKey(stusub => new { stusub.StudentId, stusub.SubjectId });
            modelBuilder.Entity<StudentSubject>()
                .HasOne(stusub => stusub.Student)
                .WithMany(stu => stu.Subjects)
                .HasForeignKey(stusub => stusub.StudentId);
            modelBuilder.Entity<StudentSubject>()
                .HasOne(stusub => stusub.Subject)
                .WithMany(sub => sub.Students)
                .HasForeignKey(stusub => stusub.SubjectId);

            

            modelBuilder.Entity<Subject>()
                .HasOne(sub => sub.Grade)
                .WithOne(gra => gra.Subject)
                .HasForeignKey<Grade>(gra => gra.SubjectId);
                


            modelBuilder.Entity<Course>()
                .HasOne(cou => cou.CategoryCourse)
                .WithMany(cat => cat.Courses)
                .HasForeignKey(cou => cou.CategoryCourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            

        modelBuilder.Entity<Course>()
                .HasOne(cou => cou.Turn)
                .WithMany(tur => tur.Courses)
                .HasForeignKey(cou => cou.TurnId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Course>()
                .HasMany(cou => cou.Subjects)
                .WithOne(sub => sub.Course)
                .HasForeignKey(sub => sub.CourseId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Subject>()
                .HasOne(sub => sub.Teacher)
                .WithMany(tea => tea.Subjects)
                .HasForeignKey(sub => sub.TeacherId)
                .OnDelete(DeleteBehavior.SetNull);

            
                
    } 
}

}
