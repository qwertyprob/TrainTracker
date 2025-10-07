using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainTracker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class IsActive_Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Stations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Incidents",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Stations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Incidents");
        }
    }
}
