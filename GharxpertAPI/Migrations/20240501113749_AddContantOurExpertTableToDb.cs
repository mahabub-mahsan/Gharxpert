using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GharxpertAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddContantOurExpertTableToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactOurExperts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Landline = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactOurExperts");
        }
    }
}
