using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UltimateBrain.Models;

public partial class UltimateBrainContext : DbContext
{
    public UltimateBrainContext()
    {
    }

    public UltimateBrainContext(DbContextOptions<UltimateBrainContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Participante> Participantes { get; set; }

    public virtual DbSet<PreguntaResuelta> PreguntaResueltas { get; set; }

    public virtual DbSet<Preguntum> Pregunta { get; set; }

    public virtual DbSet<Respuestum> Respuesta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        //We use default options
    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
     //   => optionsBuilder.UseSqlServer("Server=tcp:ultimate-br.database.windows.net,1433;Initial Catalog=ultimate-brain;Persist Security Info=False;User ID=sa-admin;Password=Brayanelmejor2023;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__particip__3213E83FA6858DC8");

            entity.ToTable("participante");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.NickName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nickName");
            entity.Property(e => e.PreguntaResuelta).HasColumnName("Pregunta_resuelta");

            entity.Property(e => e.Puntaje).HasColumnName("puntaje");
        });

        modelBuilder.Entity<PreguntaResuelta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PreguntaResuelta__3213E83F8D2F70D6");

            entity.ToTable("pregunta_resuelta");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ParticipanteId).HasColumnName("participanteId");
            entity.Property(e => e.IdPregunta).HasColumnName("id_pregunta");

            entity.HasOne(d => d.Participante).WithMany(p => p.PreguntasResueltas)
                .HasForeignKey(d => d.ParticipanteId)
                .HasConstraintName("FK__partida__partici__6383C8BA");
        });

        modelBuilder.Entity<Preguntum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pregunta__3213E83FAEC31AAD");

            entity.ToTable("pregunta");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoria");
            entity.Property(e => e.Dificultad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dificultad");
            entity.Property(e => e.Respuestas).HasColumnName("respuestas");
            entity.Property(e => e.TextoPregunta)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("textoPregunta");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo");
        });

        modelBuilder.Entity<Respuestum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__respuest__3213E83F8D82F32E");

            entity.ToTable("respuesta");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.IsCorrect).HasColumnName("isCorrect");
            entity.Property(e => e.PreguntaId).HasColumnName("preguntaId");
            entity.Property(e => e.TextoRespuesta)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("textoRespuesta");

            entity.HasOne(d => d.Pregunta).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.PreguntaId)
                .HasConstraintName("FK__respuesta__pregu__60A75C0F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
