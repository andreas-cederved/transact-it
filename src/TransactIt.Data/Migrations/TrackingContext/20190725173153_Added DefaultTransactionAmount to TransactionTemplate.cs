using Microsoft.EntityFrameworkCore.Migrations;

namespace TransactIt.Data.Migrations.TrackingContext
{
    public partial class AddedDefaultTransactionAmounttoTransactionTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DefaultTransactionAmount",
                table: "TransactionTemplates",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultTransactionAmount",
                table: "TransactionTemplates");
        }
    }
}
