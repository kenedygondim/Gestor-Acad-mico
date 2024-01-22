using Gestor_Acadêmico.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Context
{
    public class GestorAcademicoContext(DbContextOptions<GestorAcademicoContext> options) : DbContext(options)
    {
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<Nota>())
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    var nota = entry.Entity;

                    nota.MediaGeral = (nota.PrimeiraAvaliacao  + nota.SegundaAvaliacao + nota.Atividades) / 3;

                    nota.Aprovado = nota.MediaGeral >= 6;
                }
            }

            return base.SaveChanges();
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<AlunoCurso> AlunoCursos { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<AlunoDisciplina> AlunoDisciplinas { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Professor> Professores { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            modelBuilder.Entity<Nota>()
            .Property(not => not.Atividades)
            .HasColumnType("decimal(4,2)");

            modelBuilder.Entity<Nota>()
            .Property(not => not.PrimeiraAvaliacao)
            .HasColumnType("decimal(4,2)");

            modelBuilder.Entity<Nota>()
            .Property(not => not.SegundaAvaliacao)
            .HasColumnType("decimal(4,2)");

            modelBuilder.Entity<Nota>()
            .Property(not => not.MediaGeral)
            .HasColumnType("decimal(4,2)");

            modelBuilder.Entity<Nota>()
            .Property(not => not.Frequencia)
            .HasColumnType("decimal(4,2)");

            modelBuilder.Entity<Aluno>()
            .Property(alu => alu.IRA)
            .HasColumnType("decimal(4,2)");

            modelBuilder.Entity<Curso>()
            .Property(cur => cur.CargaHoraria)
            .HasColumnType("decimal(8,1)");

            modelBuilder.Entity<Disciplina>()
            .Property(dis => dis.CargaHoraria)
            .HasColumnType("decimal(8,1)");



            modelBuilder.Entity<Aluno>()
                .HasIndex(alu => alu.Cpf)
                .IsUnique();

            modelBuilder.Entity<Aluno>()
                .HasIndex(alu => alu.EnderecoDeEmail)
                .IsUnique();

            modelBuilder.Entity<Professor>()
                .HasIndex(pro => pro.Cpf)
                .IsUnique();

            modelBuilder.Entity<Professor>()
                .HasIndex(pro => pro.EnderecoDeEmail)
                .IsUnique();

            modelBuilder.Entity<Curso>()
                .HasIndex(cur => cur.NomeDoCurso)
                .IsUnique();




            modelBuilder.Entity<AlunoCurso>().HasKey(alucur => new { alucur.AlunoId, alucur.CursoId });
            modelBuilder.Entity<AlunoCurso>()
                .HasOne(alucur => alucur.Aluno)
                .WithMany(alu => alu.Cursos)
                .HasForeignKey(alucur => alucur.AlunoId);
            modelBuilder.Entity<AlunoCurso>()
                .HasOne(alucur => alucur.Curso)
                .WithMany(cur => cur.Alunos)
                .HasForeignKey(alucur => alucur.CursoId);


            modelBuilder.Entity<AlunoDisciplina>().HasKey(aludis => new { aludis.AlunoId, aludis.DisciplinaId });
            modelBuilder.Entity<AlunoDisciplina>()
                .HasOne(aludis => aludis.Aluno)
                .WithMany(alu => alu.Disciplinas)
                .HasForeignKey(aludis => aludis.AlunoId);
            modelBuilder.Entity<AlunoDisciplina>()
                .HasOne(aludis => aludis.Disciplina)
                .WithMany(dis => dis.Alunos)
                .HasForeignKey(aludis => aludis.DisciplinaId);

            

            modelBuilder.Entity<Disciplina>()
                .HasOne(dis => dis.Nota)
                .WithOne(not => not.Disciplina)
                .HasForeignKey<Nota>(not => not.DisciplinaId);
                

            modelBuilder.Entity<Curso>()
                .HasMany(cur => cur.Disciplinas)
                .WithOne(dis => dis.Curso)
                .HasForeignKey(dis => dis.CursoId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<Disciplina>()
                .HasOne(dis => dis.Professor)
                .WithMany(pro => pro.Disciplinas)
                .HasForeignKey(dis => dis.ProfessorId)
                .OnDelete(DeleteBehavior.SetNull);

            
            modelBuilder.Entity<Aluno>()
                .HasMany(alu => alu.Notas)
                .WithOne(not => not.Aluno)
                .HasForeignKey(not => not.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);
    } 
}

}
