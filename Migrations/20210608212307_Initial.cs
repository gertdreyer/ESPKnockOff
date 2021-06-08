using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    ProvinceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ProvinceID);
                });

            migrationBuilder.CreateTable(
                name: "SuburbCluster",
                columns: table => new
                {
                    SuburbClusterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuburbCluster", x => x.SuburbClusterID);
                });

            migrationBuilder.CreateTable(
                name: "TimeCode",
                columns: table => new
                {
                    TimeCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCode", x => x.TimeCodeID);
                });

            migrationBuilder.CreateTable(
                name: "Municipality",
                columns: table => new
                {
                    MunicipalityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipality", x => x.MunicipalityID);
                    table.ForeignKey(
                        name: "FK_Municipality_Province_ProvinceID",
                        column: x => x.ProvinceID,
                        principalTable: "Province",
                        principalColumn: "ProvinceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoadSheddingSlot",
                columns: table => new
                {
                    LoadSheddingSlotID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfMonthID = table.Column<int>(type: "int", nullable: false),
                    StageID = table.Column<int>(type: "int", nullable: false),
                    TimeCodeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoadSheddingSlot", x => x.LoadSheddingSlotID);
                    table.ForeignKey(
                        name: "FK_LoadSheddingSlot_TimeCode_TimeCodeID",
                        column: x => x.TimeCodeID,
                        principalTable: "TimeCode",
                        principalColumn: "TimeCodeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suburb",
                columns: table => new
                {
                    SuburbID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuburbClusterID = table.Column<int>(type: "int", nullable: false),
                    MunicipalityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suburb", x => x.SuburbID);
                    table.ForeignKey(
                        name: "FK_Suburb_Municipality_MunicipalityID",
                        column: x => x.MunicipalityID,
                        principalTable: "Municipality",
                        principalColumn: "MunicipalityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Suburb_SuburbCluster_SuburbClusterID",
                        column: x => x.SuburbClusterID,
                        principalTable: "SuburbCluster",
                        principalColumn: "SuburbClusterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoadSheddingSlot_TimeCodeID",
                table: "LoadSheddingSlot",
                column: "TimeCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Municipality_ProvinceID",
                table: "Municipality",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Suburb_MunicipalityID",
                table: "Suburb",
                column: "MunicipalityID");

            migrationBuilder.CreateIndex(
                name: "IX_Suburb_SuburbClusterID",
                table: "Suburb",
                column: "SuburbClusterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoadSheddingSlot");

            migrationBuilder.DropTable(
                name: "Suburb");

            migrationBuilder.DropTable(
                name: "TimeCode");

            migrationBuilder.DropTable(
                name: "Municipality");

            migrationBuilder.DropTable(
                name: "SuburbCluster");

            migrationBuilder.DropTable(
                name: "Province");
        }
    }
}
