using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace shift_dashboard.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Delegates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PublicKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Vote = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    Producedblocks = table.Column<int>(type: "int", nullable: false),
                    Missedblocks = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    Approval = table.Column<double>(type: "float", nullable: false),
                    Productivity = table.Column<double>(type: "float", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delegates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DelegateStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    DelegateId = table.Column<int>(type: "int", nullable: false),
                    TotalVotes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelegateStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DelegateStats_Delegates_DelegateId",
                        column: x => x.DelegateId,
                        principalTable: "Delegates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PublicKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Balance = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DelegateStatId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_DelegateStats_DelegateStatId",
                        column: x => x.DelegateStatId,
                        principalTable: "DelegateStats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "Index_Address",
                table: "Accounts",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_DelegateStatId",
                table: "Accounts",
                column: "DelegateStatId");

            migrationBuilder.CreateIndex(
                name: "Index_Address1",
                table: "Delegates",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delegates_AccountId",
                table: "Delegates",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DelegateStats_DelegateId",
                table: "DelegateStats",
                column: "DelegateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delegates_Accounts_AccountId",
                table: "Delegates",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_DelegateStats_DelegateStatId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "DelegateStats");

            migrationBuilder.DropTable(
                name: "Delegates");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}