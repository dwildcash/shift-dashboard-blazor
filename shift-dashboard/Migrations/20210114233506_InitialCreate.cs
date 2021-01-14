using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace shift_dashboard.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DelegatesDB",
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
                    Productivity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelegatesDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VotersDB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PublicKey = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Balance = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotersDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DelegateDBVoterDB",
                columns: table => new
                {
                    DelegatesVoteId = table.Column<int>(type: "int", nullable: false),
                    VotersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelegateDBVoterDB", x => new { x.DelegatesVoteId, x.VotersId });
                    table.ForeignKey(
                        name: "FK_DelegateDBVoterDB_DelegatesDB_DelegatesVoteId",
                        column: x => x.DelegatesVoteId,
                        principalTable: "DelegatesDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DelegateDBVoterDB_VotersDB_VotersId",
                        column: x => x.VotersId,
                        principalTable: "VotersDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DelegateDBVoterDB_VotersId",
                table: "DelegateDBVoterDB",
                column: "VotersId");

            migrationBuilder.CreateIndex(
                name: "Index_Address",
                table: "DelegatesDB",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Index_Address1",
                table: "VotersDB",
                column: "Address",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DelegateDBVoterDB");

            migrationBuilder.DropTable(
                name: "DelegatesDB");

            migrationBuilder.DropTable(
                name: "VotersDB");
        }
    }
}
