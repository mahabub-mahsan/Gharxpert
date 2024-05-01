using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GharxpertAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConstructionTypes",
                columns: table => new
                {
                    Cno = table.Column<int>(type: "int", nullable: false),
                    Ctype = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionTypes", x => x.Cno);
                });

            migrationBuilder.CreateTable(
                name: "ContactOurExperts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Landline = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ConstructionWork = table.Column<bool>(type: "bit", nullable: false),
                    ElectricalWork = table.Column<bool>(type: "bit", nullable: false),
                    PlumbingWork = table.Column<bool>(type: "bit", nullable: false),
                    Doors = table.Column<bool>(type: "bit", nullable: false),
                    Windows = table.Column<bool>(type: "bit", nullable: false),
                    Tiles = table.Column<bool>(type: "bit", nullable: false),
                    Granite = table.Column<bool>(type: "bit", nullable: false),
                    FalseCeiling = table.Column<bool>(type: "bit", nullable: false),
                    Paint = table.Column<bool>(type: "bit", nullable: false),
                    MS_And_SS_Work = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactOurExperts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Landline = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "LocalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuatationStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuatationStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    WorkName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkIsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_LocalUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "LocalUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Work_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WorkDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    WorkID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkTypes_Works_WorkID",
                        column: x => x.WorkID,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationMasters",
                columns: table => new
                {
                    QId = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_QuotationMasters_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuotationMasters_LocalUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "LocalUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_QuotationMasters_QuatationStatuses_QSId",
                        column: x => x.QSId,
                        principalTable: "QuatationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_QuotationMasters_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_QuotationMasters_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCharges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ConstructionTypeId = table.Column<int>(type: "int", nullable: false),
                    WorkId = table.Column<int>(type: "int", nullable: false),
                    WorkTypeId = table.Column<int>(type: "int", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceCharges_ConstructionTypes_ConstructionTypeId",
                        column: x => x.ConstructionTypeId,
                        principalTable: "ConstructionTypes",
                        principalColumn: "Cno",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ServiceCharges_LocalUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "LocalUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ServiceCharges_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ServiceCharges_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuotationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_QuotationDetails_WorkTypes_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "WorkTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuotationDetails_QId",
                table: "QuotationDetails",
                column: "QId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationDetails_WorkTypeId",
                table: "QuotationDetails",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationMasters_CustomerId",
                table: "QuotationMasters",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationMasters_QSId",
                table: "QuotationMasters",
                column: "QSId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationMasters_UserId",
                table: "QuotationMasters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationMasters_WorkId",
                table: "QuotationMasters",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_QuotationMasters_WorkTypeId",
                table: "QuotationMasters",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_ConstructionTypeId",
                table: "ServiceCharges",
                column: "ConstructionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_UserId",
                table: "ServiceCharges",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_WorkId",
                table: "ServiceCharges",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCharges_WorkTypeId",
                table: "ServiceCharges",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_UserId",
                table: "Works",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTypes_WorkID",
                table: "WorkTypes",
                column: "WorkID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactOurExperts");

            migrationBuilder.DropTable(
                name: "QuotationDetails");

            migrationBuilder.DropTable(
                name: "ServiceCharges");

            migrationBuilder.DropTable(
                name: "QuotationMasters");

            migrationBuilder.DropTable(
                name: "ConstructionTypes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "QuatationStatuses");

            migrationBuilder.DropTable(
                name: "WorkTypes");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropTable(
                name: "LocalUsers");
        }
    }
}
