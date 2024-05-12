using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrcaProject.Migrations
{
    /// <inheritdoc />
    public partial class IncludedIsClosedFlagInOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsClosed",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsClosed",
                table: "Orders");
        }
    }
}
