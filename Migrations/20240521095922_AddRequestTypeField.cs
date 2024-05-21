using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrcaProject.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestTypeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequestType",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Requestname",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestType",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Requestname",
                table: "Requests");
        }
    }
}
