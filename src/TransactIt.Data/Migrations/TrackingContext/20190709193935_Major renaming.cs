using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactIt.Data.Migrations.TrackingContext
{
    public partial class Majorrenaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntries_FinancialTransactions_FinancialTransactionId",
                table: "AccountingEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntries_LedgerAccounts_LedgerAccountId",
                table: "AccountingEntries");

            migrationBuilder.DropTable(
                name: "AccountingTemplateRules");

            migrationBuilder.DropTable(
                name: "FinancialTransactions");

            migrationBuilder.DropTable(
                name: "AccountingTemplates");

            migrationBuilder.DropTable(
                name: "LedgerAccounts");

            migrationBuilder.DropTable(
                name: "LedgerSubAccountGroups");

            migrationBuilder.DropTable(
                name: "LedgerMainAccountGroups");

            migrationBuilder.CreateTable(
                name: "MainAccountGroups",
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
                    table.PrimaryKey("PK_MainAccountGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainAccountGroups_Ledgers_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
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
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Ledgers_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DefaultTransactionDescription = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    LedgerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionTemplates_Ledgers_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubAccountGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    MainAccountGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubAccountGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubAccountGroups_MainAccountGroups_MainAccountGroupId",
                        column: x => x.MainAccountGroupId,
                        principalTable: "MainAccountGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    SubAccountGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_SubAccountGroups_SubAccountGroupId",
                        column: x => x.SubAccountGroupId,
                        principalTable: "SubAccountGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTemplateRules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Multiplier = table.Column<decimal>(type: "Money", nullable: false),
                    Side = table.Column<int>(nullable: false),
                    AccountId = table.Column<int>(nullable: false),
                    TransactionTemplateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTemplateRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionTemplateRules_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionTemplateRules_TransactionTemplates_TransactionTemplateId",
                        column: x => x.TransactionTemplateId,
                        principalTable: "TransactionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_SubAccountGroupId_Number",
                table: "Accounts",
                columns: new[] { "SubAccountGroupId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainAccountGroups_LedgerId_Number",
                table: "MainAccountGroups",
                columns: new[] { "LedgerId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubAccountGroups_MainAccountGroupId_Number",
                table: "SubAccountGroups",
                columns: new[] { "MainAccountGroupId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LedgerId",
                table: "Transactions",
                column: "LedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTemplateRules_AccountId",
                table: "TransactionTemplateRules",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTemplateRules_TransactionTemplateId",
                table: "TransactionTemplateRules",
                column: "TransactionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionTemplates_LedgerId",
                table: "TransactionTemplates",
                column: "LedgerId");

            migrationBuilder.RenameColumn(
                name: "LedgerAccountId",
                table: "AccountingEntries",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "FinancialTransactionId",
                table: "AccountingEntries",
                newName: "TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountingEntries_LedgerAccountId",
                table: "AccountingEntries",
                newName: "IX_AccountingEntries_AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountingEntries_FinancialTransactionId",
                table: "AccountingEntries",
                newName: "IX_AccountingEntries_TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntries_Accounts_AccountId",
                table: "AccountingEntries",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntries_Transactions_TransactionId",
                table: "AccountingEntries",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntries_Accounts_AccountId",
                table: "AccountingEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountingEntries_Transactions_TransactionId",
                table: "AccountingEntries");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionTemplateRules");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "TransactionTemplates");

            migrationBuilder.DropTable(
                name: "SubAccountGroups");

            migrationBuilder.DropTable(
                name: "MainAccountGroups");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "AccountingEntries",
                newName: "LedgerAccountId");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "AccountingEntries",
                newName: "FinancialTransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountingEntries_TransactionId",
                table: "AccountingEntries",
                newName: "IX_AccountingEntries_LedgerAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountingEntries_AccountId",
                table: "AccountingEntries",
                newName: "IX_AccountingEntries_FinancialTransactionId");

            migrationBuilder.CreateTable(
                name: "AccountingTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    DefaultFinancialTransactionDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LedgerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountingTemplates_Ledgers_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Description = table.Column<string>(nullable: true),
                    IdentifyingCode = table.Column<int>(nullable: false),
                    LedgerId = table.Column<int>(nullable: false),
                    TransactionDate = table.Column<DateTime>(nullable: false)
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
                name: "LedgerMainAccountGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Description = table.Column<string>(nullable: true),
                    LedgerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerMainAccountGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerMainAccountGroups_Ledgers_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerSubAccountGroups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Description = table.Column<string>(nullable: true),
                    LedgerMainAccountGroupId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerSubAccountGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerSubAccountGroups_LedgerMainAccountGroups_LedgerMainAccountGroupId",
                        column: x => x.LedgerMainAccountGroupId,
                        principalTable: "LedgerMainAccountGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    Description = table.Column<string>(nullable: true),
                    LedgerSubAccountGroupId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerAccounts_LedgerSubAccountGroups_LedgerSubAccountGroupId",
                        column: x => x.LedgerSubAccountGroupId,
                        principalTable: "LedgerSubAccountGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountingTemplateRules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountingTemplateId = table.Column<int>(nullable: false),
                    LedgerAccountId = table.Column<int>(nullable: false),
                    Multiplier = table.Column<decimal>(nullable: false),
                    Side = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountingTemplateRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountingTemplateRules_AccountingTemplates_AccountingTemplateId",
                        column: x => x.AccountingTemplateId,
                        principalTable: "AccountingTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountingTemplateRules_LedgerAccounts_LedgerAccountId",
                        column: x => x.LedgerAccountId,
                        principalTable: "LedgerAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTemplateRules_AccountingTemplateId",
                table: "AccountingTemplateRules",
                column: "AccountingTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTemplateRules_LedgerAccountId",
                table: "AccountingTemplateRules",
                column: "LedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingTemplates_LedgerId",
                table: "AccountingTemplates",
                column: "LedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_LedgerId",
                table: "FinancialTransactions",
                column: "LedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccounts_LedgerSubAccountGroupId_Number",
                table: "LedgerAccounts",
                columns: new[] { "LedgerSubAccountGroupId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LedgerMainAccountGroups_LedgerId_Number",
                table: "LedgerMainAccountGroups",
                columns: new[] { "LedgerId", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LedgerSubAccountGroups_LedgerMainAccountGroupId_Number",
                table: "LedgerSubAccountGroups",
                columns: new[] { "LedgerMainAccountGroupId", "Number" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntries_FinancialTransactions_FinancialTransactionId",
                table: "AccountingEntries",
                column: "FinancialTransactionId",
                principalTable: "FinancialTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountingEntries_LedgerAccounts_LedgerAccountId",
                table: "AccountingEntries",
                column: "LedgerAccountId",
                principalTable: "LedgerAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
