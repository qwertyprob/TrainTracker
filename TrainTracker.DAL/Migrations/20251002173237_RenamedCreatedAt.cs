using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainTracker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamedCreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdate",
                table: "Trains",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Trains",
                newName: "LastUpdate");
        }
    }
}
