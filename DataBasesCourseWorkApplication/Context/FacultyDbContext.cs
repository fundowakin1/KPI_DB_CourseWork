using DataBasesCourseWorkApplication.Models;
using Microsoft.EntityFrameworkCore;


namespace DataBasesCourseWorkApplication.Context
{
    public partial class FacultyDbContext : DbContext
    {
        public FacultyDbContext()
        {
        }

        public FacultyDbContext(DbContextOptions<FacultyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dormitory> Dormitories { get; set; }
        public virtual DbSet<DormitoryOfFaculty> DormitoryOfFaculties { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseSqlServer("Server=localhost; Database=FacultyDb;Trusted_Connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Dormitory>(entity =>
            {
                entity.ToTable("Dormitory");

                entity.HasIndex(e => e.Number, "AK_Dormitory_Number")
                    .IsUnique();

                entity.Property(e => e.CommandantFullName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CommandantPhoneNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<DormitoryOfFaculty>(entity =>
            {
                entity.HasKey(e => new { e.DormitoryId, e.FacultyId })
                    .HasName("PK__Dormitor__E71E731E81DE4DE0");

                entity.ToTable("DormitoryOfFaculty");

                entity.HasOne(d => d.Dormitory)
                    .WithMany(p => p.DormitoryOfFaculties)
                    .HasForeignKey(d => d.DormitoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DormitoryOfFaculty_Dormitory");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.DormitoryOfFaculties)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DormitoryOfFaculty_Faculty");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("Faculty");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.HasIndex(e => new { e.Number, e.DormitoryId }, "AK_Room_Number_DormitoryId")
                    .IsUnique();

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.HasOne(d => d.Dormitory)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.DormitoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Room_Dormitory");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Faculty");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Student_Room");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
