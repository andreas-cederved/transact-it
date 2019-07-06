using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactIt.Data.Migrations.TrackingContext
{
    public partial class AddedAccountingTemplateandrules : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountingTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    DefaultFinancialTransactionDescription = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    LedgerId = table.Column<int>(nullable: false)
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
                name: "AccountingTemplateRules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Multiplier = table.Column<decimal>(nullable: false),
                    Side = table.Column<int>(nullable: false),
                    LedgerAccountId = table.Column<int>(nullable: false),
                    AccountingTemplateId = table.Column<int>(nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountingTemplateRules");

            migrationBuilder.DropTable(
                name: "AccountingTemplates");
        }
    }
}
