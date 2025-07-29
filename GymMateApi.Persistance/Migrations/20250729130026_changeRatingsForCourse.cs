using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymMateApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeRatingsForCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Courses",
                newName: "Ratings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ratings",
                table: "Courses",
                newName: "Rating");
        }
    }
}
