using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Technovert.BankApp.Services.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "SourceAccountId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DestinationAccountId",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "BankId", "CreatedBy", "CreatedOn", "DefaultCurrencyCode", "IMPSToOther", "IMPSToSame", "Name", "RTGSToOther", "RTGSToSame", "UpdatedBy", "UpdatedOn" },
                values: new object[] { "abc", "admin", new DateTime(2021, 12, 22, 22, 31, 51, 745, DateTimeKind.Local).AddTicks(3493), null, 0.07m, 0.03m, "New Bank", 0.05m, 0.0m, "admin", new DateTime(2021, 12, 22, 22, 31, 51, 746, DateTimeKind.Local).AddTicks(1922) });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AccountId", "Balance", "BankId", "CurrencyCode", "Gender", "Name", "Password", "Status", "Type" },
                values: new object[] { "abc", 20m, "abc", null, 2, "John Doe", "1234", 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DestinationAccountId",
                table: "Transactions",
                column: "DestinationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SourceAccountId",
                table: "Transactions",
                column: "SourceAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_DestinationAccountId",
                table: "Transactions",
                column: "DestinationAccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_SourceAccountId",
                table: "Transactions",
                column: "SourceAccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_DestinationAccountId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_SourceAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_DestinationAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SourceAccountId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "AccountId",
                keyValue: "abc");

            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "BankId",
                keyValue: "abc");

            migrationBuilder.AlterColumn<string>(
                name: "SourceAccountId",
                table: "Transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DestinationAccountId",
                table: "Transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Transactions",
                type: "varchar(767)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
