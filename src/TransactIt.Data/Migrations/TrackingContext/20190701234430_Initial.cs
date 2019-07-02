using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactIt.Data.Migrations.TrackingContext
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ledgers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ledgers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdentifyingCode = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    LedgerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialTransactions_Ledgers_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerAccountGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    LedgerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccountGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerAccountGroups_Ledgers_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    LedgerAccountGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerAccounts_LedgerAccountGroups_LedgerAccountGroupId",
                        column: x => x.LedgerAccountGroupId,
                        principalTable: "LedgerAccountGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountingEntries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Side = table.Column<int>(nullable: false),
                    Amount = table.Column<decimal>(type: "Money", nullable: false),
                    LedgerAccountId = table.Column<int>(nullable: false),
                    FinancialTransactionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountingEntries_FinancialTransactions_FinancialTransactionId",
                        column: x => x.FinancialTransactionId,
                        principalTable: "FinancialTransactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountingEntries_LedgerAccounts_LedgerAccountId",
                        column: x => x.LedgerAccountId,
                        principalTable: "LedgerAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntries_FinancialTransactionId",
                table: "AccountingEntries",
                column: "FinancialTransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingEntries_LedgerAccountId",
                table: "AccountingEntries",
                column: "LedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_LedgerId",
                table: "FinancialTransactions",
                column: "LedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccountGroups_LedgerId",
                table: "LedgerAccountGroups",
                column: "LedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccounts_LedgerAccountGroupId",
                table: "LedgerAccounts",
                column: "LedgerAccountGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountingEntries");

            migrationBuilder.DropTable(
                name: "FinancialTransactions");

            migrationBuilder.DropTable(
                name: "LedgerAccounts");

            migrationBuilder.DropTable(
                name: "LedgerAccountGroups");

            migrationBuilder.DropTable(
                name: "Ledgers");
        }
    }
}
