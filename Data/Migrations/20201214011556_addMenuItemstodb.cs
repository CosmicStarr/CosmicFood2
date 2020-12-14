using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addMenuItemstodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GetMenuItems",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false),
                    FoodTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetMenuItems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_GetMenuItems_GetCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "GetCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GetMenuItems_GetFoodTypes_FoodTypeID",
                        column: x => x.FoodTypeID,
                        principalTable: "GetFoodTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GetMenuItems_CategoryID",
                table: "GetMenuItems",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_GetMenuItems_FoodTypeID",
                table: "GetMenuItems",
                column: "FoodTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetMenuItems");
        }
    }
}
