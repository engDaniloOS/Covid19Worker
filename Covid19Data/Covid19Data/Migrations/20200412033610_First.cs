using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid19Data.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayDatas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastUpdatedAtSource = table.Column<DateTime>(nullable: false),
                    SourceUrl = table.Column<string>(nullable: true),
                    Infected = table.Column<int>(nullable: false),
                    Deceased = table.Column<int>(nullable: false),
                    TotalTested = table.Column<int>(nullable: false),
                    TotalNotInfected = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DestinationEmails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationEmails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateInfos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    DayDataId = table.Column<long>(nullable: true),
                    DayDataId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StateInfos_DayDatas_DayDataId",
                        column: x => x.DayDataId,
                        principalTable: "DayDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StateInfos_DayDatas_DayDataId1",
                        column: x => x.DayDataId1,
                        principalTable: "DayDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StateInfos_DayDataId",
                table: "StateInfos",
                column: "DayDataId");

            migrationBuilder.CreateIndex(
                name: "IX_StateInfos_DayDataId1",
                table: "StateInfos",
                column: "DayDataId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DestinationEmails");

            migrationBuilder.DropTable(
                name: "StateInfos");

            migrationBuilder.DropTable(
                name: "DayDatas");
        }
    }
}
