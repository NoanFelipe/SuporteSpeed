using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SuporteSpeed.API.Data;

public partial class SuporteSpeedDbContext : IdentityDbContext<ApiUser>
{
    public SuporteSpeedDbContext()
    {
    }

    public SuporteSpeedDbContext(DbContextOptions<SuporteSpeedDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airesponse> Airesponses { get; set; }

    public virtual DbSet<HumanResponse> HumanResponses { get; set; }

    public virtual DbSet<SupportTicket> SupportTickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=SuporteSpeedDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Airesponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AIRespon__3213E83FB77250D7");

            entity.ToTable("AIResponses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Confidence).HasColumnName("confidence");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.ModelName)
                .HasMaxLength(100)
                .HasColumnName("model_name");
            entity.Property(e => e.RespondedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("responded_at");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.Ticket).WithMany(p => p.Airesponses)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("fk_airesponses_supporttickets");
        });

        modelBuilder.Entity<HumanResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HumanRes__3213E83F027E8DC2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Message).HasColumnName("message");
            entity.Property(e => e.RespondedAt)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("responded_at");
            entity.Property(e => e.SupportAgentId).HasColumnName("support_agent_id");
            entity.Property(e => e.TicketId).HasColumnName("ticket_id");

            entity.HasOne(d => d.SupportAgent).WithMany(p => p.HumanResponses)
                .HasForeignKey(d => d.SupportAgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_humanresponses_users");

            entity.HasOne(d => d.Ticket).WithMany(p => p.HumanResponses)
                .HasForeignKey(d => d.TicketId)
                .HasConstraintName("fk_humanresponses_supporttickets");
        });

        modelBuilder.Entity<SupportTicket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3213E83F5F38289B");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Field)
                .HasMaxLength(100)
                .HasColumnName("field");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.SupportTickets)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_supporttickets_users");
        });

        modelBuilder.Entity<ApiUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07D882AC4B");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Enrollment)
                .HasMaxLength(50)
                .HasColumnName("enrollment");
            entity.Property(e => e.Field)
                .HasMaxLength(50)
                .HasColumnName("field");
        });

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = "27c859b2-13b1-49f2-a311-e153cc7d42f9",
                ConcurrencyStamp = "STATIC-CONCURRENCY-STAMP-ROLE-USER"
            },    
            new IdentityRole
            {
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                Id = "8c3bde9d-dbd2-4f5a-91be-8b435cc72e67",
                ConcurrencyStamp = "STATIC-CONCURRENCY-STAMP-ROLE-ADMIN"
            }
        );

        modelBuilder.Entity<ApiUser>().HasData(
            new ApiUser
            {
                Id = "cfaa508f-4817-4149-9d96-18de505c1be8",
                Email = "user@suportespeed.com",
                NormalizedEmail = "USER@SUPORTESPEED.COM",
                UserName = "user@suportespeed.com",
                NormalizedUserName = "USER@SUPORTESPEED.COM",
                Name = "System User",
                Enrollment = "Tech University",
                Field = "Tech",
                PasswordHash = "AQAAAAIAAYagAAAAEENWXDp+lc3xmS1O+j+KsXlpVHbTeLZQo0/dCYx+tXwlkr9XnUFVe7Ljw6h7au0SyA==", //Hashed password for "P@ssword1"
                SecurityStamp = "STATIC-SECURITY-STAMP-USER",
                ConcurrencyStamp = "STATIC-CONCURRENCY-STAMP-USER"
            },
            new ApiUser
            {
                Id = "d5c07402-2935-48e1-a9c1-fe50ea56c080",
                Email = "admin@suportespeed.com",
                NormalizedEmail = "ADMIN@SUPORTESPEED.COM",
                UserName = "admin@suportespeed.com",
                NormalizedUserName = "ADMIN@SUPORTESPEED.COM",
                Name = "System Admin",
                Enrollment = "Tech University",
                Field = "Tech",
                PasswordHash = "AQAAAAIAAYagAAAAEENWXDp+lc3xmS1O+j+KsXlpVHbTeLZQo0/dCYx+tXwlkr9XnUFVe7Ljw6h7au0SyA==",
                SecurityStamp = "STATIC-SECURITY-STAMP-ADMIN",
                ConcurrencyStamp = "STATIC-CONCURRENCY-STAMP-ADMIN"
            }    
        );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "27c859b2-13b1-49f2-a311-e153cc7d42f9",
                UserId = "cfaa508f-4817-4149-9d96-18de505c1be8"
            },
            new IdentityUserRole<string>
            {
                RoleId = "8c3bde9d-dbd2-4f5a-91be-8b435cc72e67",
                UserId = "d5c07402-2935-48e1-a9c1-fe50ea56c080"
            }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
