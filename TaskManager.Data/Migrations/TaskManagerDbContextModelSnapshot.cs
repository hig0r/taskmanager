﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskManager.Data;

#nullable disable

namespace TaskManager.Data.Migrations
{
    [DbContext(typeof(TaskManagerDbContext))]
    partial class TaskManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskManager.Domain.Entities.Projeto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CriadoPorId")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CriadoPorId");

                    b.ToTable("Projetos");
                });

            modelBuilder.Entity("TaskManager.Domain.Entities.Tarefa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Instant>("CriadoEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CriadoPorId")
                        .HasColumnType("integer");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Prioridade")
                        .HasColumnType("integer");

                    b.Property<int>("ProjetoId")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<LocalDate>("Vencimento")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CriadoPorId");

                    b.HasIndex("ProjetoId");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("TaskManager.Domain.Entities.TarefaAlteracao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Instant>("CriadoEm")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("CriadoPorId")
                        .HasColumnType("integer");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("TarefaId")
                        .HasColumnType("integer");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<LocalDate>("Vencimento")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CriadoPorId");

                    b.HasIndex("TarefaId");

                    b.ToTable("TarefaAlteracao");
                });

            modelBuilder.Entity("TaskManager.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Funcao")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Funcao = 0,
                            Nome = "João"
                        },
                        new
                        {
                            Id = 2,
                            Funcao = 1,
                            Nome = "Maria"
                        });
                });

            modelBuilder.Entity("TaskManager.Domain.Entities.Projeto", b =>
                {
                    b.HasOne("TaskManager.Domain.Entities.Usuario", "CriadoPor")
                        .WithMany()
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CriadoPor");
                });

            modelBuilder.Entity("TaskManager.Domain.Entities.Tarefa", b =>
                {
                    b.HasOne("TaskManager.Domain.Entities.Usuario", "CriadoPor")
                        .WithMany()
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManager.Domain.Entities.Projeto", "Projeto")
                        .WithMany("Tarefas")
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("TaskManager.Domain.Entities.Comentario", "Comentarios", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<int>("CriadoPorId")
                                .HasColumnType("integer");

                            b1.Property<int>("TarefaId")
                                .HasColumnType("integer");

                            b1.Property<string>("Texto")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("Id");

                            b1.HasIndex("CriadoPorId");

                            b1.HasIndex("TarefaId");

                            b1.ToTable("Tarefas_Comentarios");

                            b1.HasOne("TaskManager.Domain.Entities.Usuario", "CriadoPor")
                                .WithMany()
                                .HasForeignKey("CriadoPorId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("TarefaId");

                            b1.Navigation("CriadoPor");
                        });

                    b.Navigation("Comentarios");

                    b.Navigation("CriadoPor");

                    b.Navigation("Projeto");
                });

            modelBuilder.Entity("TaskManager.Domain.Entities.TarefaAlteracao", b =>
                {
                    b.HasOne("TaskManager.Domain.Entities.Usuario", "CriadoPor")
                        .WithMany()
                        .HasForeignKey("CriadoPorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskManager.Domain.Entities.Tarefa", "Tarefa")
                        .WithMany("Alteracoes")
                        .HasForeignKey("TarefaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("TaskManager.Domain.Entities.Comentario", "Comentarios", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<int>("CriadoPorId")
                                .HasColumnType("integer");

                            b1.Property<int>("TarefaAlteracaoId")
                                .HasColumnType("integer");

                            b1.Property<string>("Texto")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("Id");

                            b1.HasIndex("CriadoPorId");

                            b1.HasIndex("TarefaAlteracaoId");

                            b1.ToTable("TarefaAlteracao_Comentarios");

                            b1.HasOne("TaskManager.Domain.Entities.Usuario", "CriadoPor")
                                .WithMany()
                                .HasForeignKey("CriadoPorId")
                                .OnDelete(DeleteBehavior.Cascade)
                                .IsRequired();

                            b1.WithOwner()
                                .HasForeignKey("TarefaAlteracaoId");

                            b1.Navigation("CriadoPor");
                        });

                    b.Navigation("Comentarios");

                    b.Navigation("CriadoPor");

                    b.Navigation("Tarefa");
                });

            modelBuilder.Entity("TaskManager.Domain.Entities.Projeto", b =>
                {
                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("TaskManager.Domain.Entities.Tarefa", b =>
                {
                    b.Navigation("Alteracoes");
                });
#pragma warning restore 612, 618
        }
    }
}
