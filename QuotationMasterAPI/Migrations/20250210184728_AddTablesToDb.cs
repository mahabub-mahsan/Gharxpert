using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuotationMasterAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuotationMasters",
                columns: table => new
                {
                    QId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkId = table.Column<int>(type: "int", nullable: false),
                    QDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    WorkTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QSId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationMasters", x => x.QId);
                });

            migrationBuilder.CreateTable(
                name: "QuotationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QId = table.Column<int>(type: "int", nullable: false),
                    WorkTypeId = table.Column<int>(type: "int", nullable: false),
                    lenth_in_feets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    width_in_feets = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    slab_area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    no_of_floors = table.Column<int>(type: "int", nullable: false),
                    total_area = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuoteId = table.Column<int>(type: "int", nullable: false),
                    StructuralWorkGroundFloor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuotationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuotationDetails_QuotationMasters_QId",
                        column: x => x.QId,
                        principalTable: "QuotationMasters",
                        principalColumn: "QId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotationDetails_QId",
                table: "QuotationDetails",
                column: "QId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuotationDetails");

            migrationBuilder.DropTable(
                name: "QuotationMasters");
        }
    }
}
