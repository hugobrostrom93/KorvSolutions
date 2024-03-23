using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KorvSolutions.Migrations
{
    /// <inheritdoc />
    public partial class IdforPlanProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PlanProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "PlanProducts");
        }
    }
}
