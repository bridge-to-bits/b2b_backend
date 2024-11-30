using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users.Data.Migrations
{
    /// <inheritdoc />
    public partial class changefirstnametousername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.Sql("UPDATE Users SET Username = FirstName");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(100)",
                nullable: true);

            migrationBuilder.Sql("UPDATE Users SET FirstName = Username");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");
        }

    }
}
