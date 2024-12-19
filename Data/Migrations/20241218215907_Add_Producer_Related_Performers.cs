using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Producer_Related_Performers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProducersPerformers",
                columns: table => new
                {
                    RelatedPerformersId = table.Column<Guid>(type: "char(36)", nullable: false),
                    RelatedProducersId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProducersPerformers", x => new { x.RelatedPerformersId, x.RelatedProducersId });
                    table.ForeignKey(
                        name: "FK_ProducersPerformers_Performers_RelatedPerformersId",
                        column: x => x.RelatedPerformersId,
                        principalTable: "Performers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProducersPerformers_Producers_RelatedProducersId",
                        column: x => x.RelatedProducersId,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProducersPerformers_RelatedProducersId",
                table: "ProducersPerformers",
                column: "RelatedProducersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProducersPerformers");
        }
    }
}
