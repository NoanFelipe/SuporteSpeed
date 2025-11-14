using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SuporteSpeed.API.Data;

public partial class SuporteSpeedDbContext : DbContext
{
    public SuporteSpeedDbContext()
    {
    }

    public SuporteSpeedDbContext(DbContextOptions<SuporteSpeedDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AiResponse> AiResponses { get; set; }

    public virtual DbSet<HumanResponse> HumanResponses { get; set; }

    public virtual DbSet<SupportTicket> SupportTickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AiResponse>(entity =>
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
                .HasDefaultValueSql("(sysdatetime())")
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
                .HasDefaultValueSql("(sysdatetime())")
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07D882AC4B");

            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Enrollment)
                .HasMaxLength(50)
                .HasColumnName("enrollment");
            entity.Property(e => e.Field)
                .HasMaxLength(50)
                .HasColumnName("field");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.UserType)
                .HasMaxLength(20)
                .HasColumnName("user_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
