using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMaterialClassIdAndCreateManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_Classes_ClassId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_ClassId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Materials");

            migrationBuilder.CreateTable(
                name: "ClassMaterial",
                columns: table => new
                {
                    ClassesId = table.Column<int>(type: "integer", nullable: false),
                    MaterialsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassMaterial", x => new { x.ClassesId, x.MaterialsId });
                    table.ForeignKey(
                        name: "FK_ClassMaterial_Classes_ClassesId",
                        column: x => x.ClassesId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassMaterial_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassMaterial_MaterialsId",
                table: "ClassMaterial",
                column: "MaterialsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassMaterial");

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Materials",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ClassId",
                table: "Materials",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_Classes_ClassId",
                table: "Materials",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
