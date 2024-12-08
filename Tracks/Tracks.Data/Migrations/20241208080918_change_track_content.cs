using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tracks.Data.Migrations
{
    /// <inheritdoc />
    public partial class change_track_content : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Tracks");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Tracks",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Tracks");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "Tracks",
                type: "longblob",
                nullable: false);
        }
    }
}
