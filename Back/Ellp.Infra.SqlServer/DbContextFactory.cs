using Microsoft.EntityFrameworkCore;
using Ellp.Api.Domain.Entities;
using Ellp.Api.Infra.SqlServer.Configurations;

namespace Ellp.Api.Infra.SqlServer
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<WorkshopAluno> WorkshopStudents { get; set; }
        public DbSet<WorkshopProfessor> WorkshopProfessors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new WorkshopConfiguration());
            modelBuilder.ApplyConfiguration(new ProfessorConfiguration());
            modelBuilder.ApplyConfiguration(new WorkshopAlunoConfiguration());
            modelBuilder.ApplyConfiguration(new WorkshopProfessorConfiguration());
        }
    }
}




