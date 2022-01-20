using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Technovert.BankApp.Services.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<string>(nullable: false),
                    SourceAccountId = table.Column<string>(nullable: true),
                    SourceBankId = table.Column<string>(nullable: true),
                    DestinationAccountId = table.Column<string>(nullable: true),
                    DestinationBankId = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    TransactionMode = table.Column<int>(nullable: false),
                    CurrencyCode = table.Column<string>(nullable: true),
                    On = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<string>(nullable: false),
                    BankId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Balance = table.Column<decimal>(nullable: false),
                    CurrencyCode = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ExchangeRate = table.Column<decimal>(nullable: false),
                    BankId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    BankId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DefaultCurrencyCode = table.Column<string>(nullable: true),
                    RTGSToSame = table.Column<decimal>(nullable: false),
                    RTGSToOther = table.Column<decimal>(nullable: false),
                    IMPSToSame = table.Column<decimal>(nullable: false),
                    IMPSToOther = table.Column<decimal>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankId);
                    table.ForeignKey(
                        name: "FK_Banks_Currencies_DefaultCurrencyCode",
                        column: x => x.DefaultCurrencyCode,
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BankId",
                table: "Accounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyCode",
                table: "Accounts",
                column: "CurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Banks_DefaultCurrencyCode",
                table: "Banks",
                column: "DefaultCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_BankId",
                table: "Currencies",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CurrencyCode",
                table: "Transactions",
                column: "CurrencyCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Currencies_CurrencyCode",
                table: "Transactions",
                column: "CurrencyCode",
                principalTable: "Currencies",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Banks_BankId",
                table: "Accounts",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Currencies_CurrencyCode",
                table: "Accounts",
                column: "CurrencyCode",
                principalTable: "Currencies",
                principalColumn: "Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_Banks_BankId",
                table: "Currencies",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_Banks_BankId",
                table: "Currencies");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
