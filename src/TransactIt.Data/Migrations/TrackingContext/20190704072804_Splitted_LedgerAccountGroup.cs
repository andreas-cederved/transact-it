using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactIt.Data.Migrations.TrackingContext
{
    public partial class Splitted_LedgerAccountGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerAccounts_LedgerAccountGroups_LedgerAccountGroupId",
                table: "LedgerAccounts");

            migrationBuilder.DropTable(
                name: "LedgerAccountGroups");

            migrationBuilder.DropIndex(
                name: "IX_LedgerAccounts_LedgerAccountGroupId",
                table: "LedgerAccounts");

            migrationBuilder.RenameColumn(
                name: "LedgerAccountGroupId",
                table: "LedgerAccounts",
                newName: "LedgerSubAccountGroupId");

            migrationBuilder.CreateTable(
                name: "LedgerMainAccountGroups",
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
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    LedgerMainAccountGroupId = table.Column<int>(nullable: false)
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
                name: "FK_LedgerAccounts_LedgerSubAccountGroups_LedgerSubAccountGroupId",
                table: "LedgerAccounts",
                column: "LedgerSubAccountGroupId",
                principalTable: "LedgerSubAccountGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerAccounts_LedgerSubAccountGroups_LedgerSubAccountGroupId",
                table: "LedgerAccounts");

            migrationBuilder.DropTable(
                name: "LedgerSubAccountGroups");

            migrationBuilder.DropTable(
                name: "LedgerMainAccountGroups");

            migrationBuilder.DropIndex(
                name: "IX_LedgerAccounts_LedgerSubAccountGroupId_Number",
                table: "LedgerAccounts");

            migrationBuilder.RenameColumn(
                name: "LedgerSubAccountGroupId",
                table: "LedgerAccounts",
                newName: "LedgerAccountGroupId");

            migrationBuilder.CreateTable(
                name: "LedgerAccountGroups",
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
                    table.PrimaryKey("PK_LedgerAccountGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerAccountGroups_Ledgers_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccounts_LedgerAccountGroupId",
                table: "LedgerAccounts",
                column: "LedgerAccountGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccountGroups_LedgerId",
                table: "LedgerAccountGroups",
                column: "LedgerId");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerAccounts_LedgerAccountGroups_LedgerAccountGroupId",
                table: "LedgerAccounts",
                column: "LedgerAccountGroupId",
                principalTable: "LedgerAccountGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
