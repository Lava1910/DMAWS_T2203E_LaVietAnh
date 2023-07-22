using DMAWS_T2203E_LaVietAnh.Entities;
using Microsoft.EntityFrameworkCore;


namespace DMAWS_T2203E_LaVietAnh.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the Employee entity with no primary key
            modelBuilder.Entity<ProjectEmployee>()
                .HasKey(pe => new { pe.EmployeeId, pe.ProjectId });

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Employees)
                .WithMany(e => e.ProjectEmployees)
                .HasForeignKey(pe => pe.EmployeeId);

            modelBuilder.Entity<ProjectEmployee>()
                .HasOne(pe => pe.Projects)
                .WithMany(p => p.ProjectEmployees)
                .HasForeignKey(pe => pe.ProjectId);
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
    }
}
