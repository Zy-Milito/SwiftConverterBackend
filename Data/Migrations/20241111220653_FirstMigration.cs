using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    ISOCode = table.Column<string>(type: "TEXT", nullable: false),
                    ExchangeRate = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    MaxConversions = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    SubscriptionPlanId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_SubscriptionPlans_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyUser",
                columns: table => new
                {
                    FavedCurrenciesId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyUser", x => new { x.FavedCurrenciesId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CurrencyUser_Currencies_FavedCurrenciesId",
                        column: x => x.FavedCurrenciesId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    FromCurrencyId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToCurrencyId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_Currencies_FromCurrencyId",
                        column: x => x.FromCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histories_Currencies_ToCurrencyId",
                        column: x => x.ToCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Histories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "ExchangeRate", "ISOCode", "Name", "Symbol" },
                values: new object[,]
                {
                    { 1, 1.0, "USD", "United States Dollar", "$" },
                    { 2, 1.1200000000000001, "EUR", "Euro", "€" },
                    { 3, 0.0091000000000000004, "JPY", "Japanese Yen", "¥" },
                    { 4, 1.25, "GBP", "Pound Sterling", "£" },
                    { 5, 0.79000000000000004, "CAD", "Canadian Dollar", "$" },
                    { 6, 0.71999999999999997, "AUD", "Australian Dollar", "$" },
                    { 7, 0.012999999999999999, "INR", "Indian Rupee", "₹" },
                    { 8, 0.001, "ARS", "Argentine Peso", "$" },
                    { 9, 0.00084000000000000003, "KRW", "South Korean Won", "₩" },
                    { 10, 0.14999999999999999, "CNY", "Chinese Yuan", "¥" }
                });

            migrationBuilder.InsertData(
                table: "SubscriptionPlans",
                columns: new[] { "Id", "MaxConversions", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 10, "Free", 0.0 },
                    { 2, 100, "Basic", 1.9099999999999999 },
                    { 3, null, "Pro", 5.1500000000000004 },
                    { 4, 100, "Trial", 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsAdmin", "Password", "SubscriptionPlanId", "Username" },
                values: new object[,]
                {
                    { 1, "admin@mail.com", true, "admin", 3, "admin" },
                    { 2, "zyther@mail.com", false, "010101", 1, "Zyther" },
                    { 3, "caleb@mail.com", false, "calebthepro", 2, "Caleb" },
                    { 4, "lumion@mail.com", false, "master", 3, "Lumion" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyUser_UserId",
                table: "CurrencyUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_FromCurrencyId",
                table: "Histories",
                column: "FromCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_ToCurrencyId",
                table: "Histories",
                column: "ToCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_UserId",
                table: "Histories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SubscriptionPlanId",
                table: "Users",
                column: "SubscriptionPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyUser");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");
        }
    }
}
