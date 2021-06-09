using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Migrations
{
    public partial class Initial_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SuburbClusterID",
                table: "LoadSheddingSlot",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LoadSheddingSlot_SuburbClusterID",
                table: "LoadSheddingSlot",
                column: "SuburbClusterID");

            migrationBuilder.AddForeignKey(
                name: "FK_LoadSheddingSlot_SuburbCluster_SuburbClusterID",
                table: "LoadSheddingSlot",
                column: "SuburbClusterID",
                principalTable: "SuburbCluster",
                principalColumn: "SuburbClusterID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoadSheddingSlot_SuburbCluster_SuburbClusterID",
                table: "LoadSheddingSlot");

            migrationBuilder.DropIndex(
                name: "IX_LoadSheddingSlot_SuburbClusterID",
                table: "LoadSheddingSlot");

            migrationBuilder.DropColumn(
                name: "SuburbClusterID",
                table: "LoadSheddingSlot");
        }
    }
}
