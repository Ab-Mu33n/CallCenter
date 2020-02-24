using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CallCenter.Models
{
    public partial class CallCenterContext : DbContext
    {
        //public CallCenterContext()
        //{
        //}

        public CallCenterContext(DbContextOptions<CallCenterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CallCategory> CallCategory { get; set; }
        public virtual DbSet<CallDetails> CallDetails { get; set; }
        public virtual DbSet<Calls> Calls { get; set; }
        public virtual DbSet<Designations> Designations { get; set; }
        public virtual DbSet<Gender> Gender { get; set; }
        public virtual DbSet<Individuals> Individuals { get; set; }
        public virtual DbSet<Staffs> Staffs { get; set; }
        public virtual DbSet<States> States { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-QK4RBFK;Database=CallCenter;Integrated Security=True");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CallCategory>(entity =>
            {
                entity.Property(e => e.CallCategoryId).HasColumnName("CallCategoryID");

                entity.Property(e => e.CallCategory1)
                    .IsRequired()
                    .HasColumnName("CallCategory")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryDetails)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CallDetails>(entity =>
            {
                entity.HasKey(e => e.CallDescriptionId);

                entity.Property(e => e.CallDescriptionId).HasColumnName("CallDescriptionID");

                entity.Property(e => e.CallCategoryId).HasColumnName("CallCategoryID");

                entity.Property(e => e.CallId).HasColumnName("CallID");

                entity.Property(e => e.CallSummary)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.CallCategory)
                    .WithMany(p => p.CallDetails)
                    .HasForeignKey(d => d.CallCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CallDetails_CallCategory");

                entity.HasOne(d => d.Call)
                    .WithMany(p => p.CallDetails)
                    .HasForeignKey(d => d.CallId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CallDetails_Calls");
            });

            modelBuilder.Entity<Calls>(entity =>
            {
                entity.HasKey(e => e.CallId);

                entity.Property(e => e.CallId).HasColumnName("CallID");

                entity.Property(e => e.CallAttendeeId).HasColumnName("CallAttendeeID");

                entity.Property(e => e.CallStateId).HasColumnName("CallStateID");

                entity.Property(e => e.CallerIndividualId).HasColumnName("CallerIndividualID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.CallAttendee)
                    .WithMany(p => p.Calls)
                    .HasForeignKey(d => d.CallAttendeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Calls_Staffs");

                entity.HasOne(d => d.CallState)
                    .WithMany(p => p.Calls)
                    .HasForeignKey(d => d.CallStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Calls_States");

                entity.HasOne(d => d.CallerIndividual)
                    .WithMany(p => p.Calls)
                    .HasForeignKey(d => d.CallerIndividualId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Calls_Individuals");
            });

            modelBuilder.Entity<Designations>(entity =>
            {
                entity.HasKey(e => e.DesignationId);

                entity.Property(e => e.DesignationId).HasColumnName("DesignationID");

                entity.Property(e => e.DesignationDetails)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DesignationName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.Gender1)
                    .IsRequired()
                    .HasColumnName("Gender")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.GenderAbbreviation)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Individuals>(entity =>
            {
                entity.HasKey(e => e.IndividualId);

                entity.Property(e => e.IndividualId).HasColumnName("IndividualID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateofBirth).HasColumnType("datetime");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.Nicnumber)
                    .HasColumnName("NICNumber")
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.Property(e => e.PassportNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Individuals)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Individuals_Gender");
            });

            modelBuilder.Entity<Staffs>(entity =>
            {
                entity.HasKey(e => e.StaffId);

                entity.Property(e => e.StaffId).HasColumnName("StaffID");

                entity.Property(e => e.DesignationId).HasColumnName("DesignationID");

                entity.Property(e => e.IndividualId).HasColumnName("IndividualID");

                entity.HasOne(d => d.Designation)
                    .WithMany(p => p.Staffs)
                    .HasForeignKey(d => d.DesignationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staffs_Designations");

                entity.HasOne(d => d.Individual)
                    .WithMany(p => p.Staffs)
                    .HasForeignKey(d => d.IndividualId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staffs_Individuals");
            });

            modelBuilder.Entity<States>(entity =>
            {
                entity.HasKey(e => e.StateId);

                entity.Property(e => e.StateId).HasColumnName("StateID");

                entity.Property(e => e.Details)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
