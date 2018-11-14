using Microsoft.EntityFrameworkCore.Migrations;

namespace WEBAfl3.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_ComponentTypes_ComponentTypeId1",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_ComponentTypeId1",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "ComponentTypeId1",
                table: "Components");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentTypeId",
                table: "Components",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentTypeId",
                table: "Components",
                column: "ComponentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_ComponentTypes_ComponentTypeId",
                table: "Components",
                column: "ComponentTypeId",
                principalTable: "ComponentTypes",
                principalColumn: "ComponentTypeId",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_ComponentTypes_ComponentTypeId",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_ComponentTypeId",
                table: "Components");

            migrationBuilder.AlterColumn<long>(
                name: "ComponentTypeId",
                table: "Components",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComponentTypeId1",
                table: "Components",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_ComponentTypeId1",
                table: "Components",
                column: "ComponentTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_ComponentTypes_ComponentTypeId1",
                table: "Components",
                column: "ComponentTypeId1",
                principalTable: "ComponentTypes",
                principalColumn: "ComponentTypeId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
