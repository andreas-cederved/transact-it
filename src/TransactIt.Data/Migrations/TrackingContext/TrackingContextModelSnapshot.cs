﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransactIt.Data.Contexts;

namespace TransactIt.Data.Migrations.TrackingContext
{
    [DbContext(typeof(TransactIt.Data.Contexts.TrackingContext))]
    partial class TrackingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview6.19304.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TransactIt.Domain.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("Number");

                    b.Property<int>("SubAccountGroupId");

                    b.HasKey("Id");

                    b.HasIndex("SubAccountGroupId", "Number")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.AccountingEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<decimal>("Amount")
                        .HasColumnType("Money");

                    b.Property<int>("Side");

                    b.Property<int>("TransactionId");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("TransactionId");

                    b.ToTable("AccountingEntries");
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.Ledger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Ledgers");
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.MainAccountGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<int>("LedgerId");

                    b.Property<string>("Name");

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.HasIndex("LedgerId", "Number")
                        .IsUnique();

                    b.ToTable("MainAccountGroups");
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.SubAccountGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<int>("MainAccountGroupId");

                    b.Property<string>("Name");

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.HasIndex("MainAccountGroupId", "Number")
                        .IsUnique();

                    b.ToTable("SubAccountGroups");
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("Description");

                    b.Property<int>("IdentifyingCode");

                    b.Property<int>("LedgerId");

                    b.Property<DateTime>("TransactionDate");

                    b.HasKey("Id");

                    b.HasIndex("LedgerId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.TransactionTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<string>("DefaultTransactionDescription");

                    b.Property<string>("Description");

                    b.Property<int>("LedgerId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LedgerId");

                    b.ToTable("TransactionTemplates");
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.TransactionTemplateRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId");

                    b.Property<decimal>("Multiplier")
                        .HasColumnType("Money");

                    b.Property<int>("Side");

                    b.Property<int>("TransactionTemplateId");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("TransactionTemplateId");

                    b.ToTable("TransactionTemplateRules");
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.Account", b =>
                {
                    b.HasOne("TransactIt.Domain.Entities.SubAccountGroup", "SubAccountGroup")
                        .WithMany("Accounts")
                        .HasForeignKey("SubAccountGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.AccountingEntry", b =>
                {
                    b.HasOne("TransactIt.Domain.Entities.Account", "Account")
                        .WithMany("AccountingEntries")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TransactIt.Domain.Entities.Transaction", "Transaction")
                        .WithMany("AccountingEntries")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.MainAccountGroup", b =>
                {
                    b.HasOne("TransactIt.Domain.Entities.Ledger", "Ledger")
                        .WithMany("MainAccountGroups")
                        .HasForeignKey("LedgerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.SubAccountGroup", b =>
                {
                    b.HasOne("TransactIt.Domain.Entities.MainAccountGroup", "MainAccountGroup")
                        .WithMany("SubAccountGroups")
                        .HasForeignKey("MainAccountGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("TransactIt.Domain.Entities.Ledger", "Ledger")
                        .WithMany("Transactions")
                        .HasForeignKey("LedgerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.TransactionTemplate", b =>
                {
                    b.HasOne("TransactIt.Domain.Entities.Ledger", "Ledger")
                        .WithMany("TransactionTemplates")
                        .HasForeignKey("LedgerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TransactIt.Domain.Entities.TransactionTemplateRule", b =>
                {
                    b.HasOne("TransactIt.Domain.Entities.Account", "Account")
                        .WithMany("TransactionTemplateRules")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("TransactIt.Domain.Entities.TransactionTemplate", "TransactionTemplate")
                        .WithMany("TransactionTemplateRules")
                        .HasForeignKey("TransactionTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
