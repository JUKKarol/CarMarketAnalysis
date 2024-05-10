using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarMarketAnalysis.Migrations
{
    /// <inheritdoc />
    public partial class SplitNameAndNameForSearch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameForSearch",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameForSearch",
                table: "Cars");
        }
    }
}