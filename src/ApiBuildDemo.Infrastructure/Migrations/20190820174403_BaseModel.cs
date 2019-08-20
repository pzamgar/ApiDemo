using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiBuildDemo.Infrastructure.Migrations
{
    public partial class BaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "Values",
                newName: "DateModified");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Values",
                newName: "DateCreated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateModified",
                table: "Values",
                newName: "ModifiedDate");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Values",
                newName: "CreatedDate");
        }
    }
}
