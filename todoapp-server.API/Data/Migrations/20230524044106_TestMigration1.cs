using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoAppServer.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_Users_UserId",
                table: "Device");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Device",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Users_UserId",
                table: "Device",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_Users_UserId",
                table: "Device");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Device",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_Users_UserId",
                table: "Device",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
