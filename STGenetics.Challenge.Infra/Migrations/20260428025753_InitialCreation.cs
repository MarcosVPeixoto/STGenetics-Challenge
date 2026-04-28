using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STGenetics.Challenge.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "challenge");

            migrationBuilder.CreateTable(
                name: "discount",
                schema: "challenge",
                columns: table => new
                {
                    discount_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    active = table.Column<bool>(type: "bit", nullable: false),
                    discount_percentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discount", x => x.discount_id);
                });

            migrationBuilder.CreateTable(
                name: "menu_item",
                schema: "challenge",
                columns: table => new
                {
                    menu_item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu_item", x => x.menu_item_id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "challenge",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                schema: "challenge",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiscountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_order_discount_DiscountId",
                        column: x => x.DiscountId,
                        principalSchema: "challenge",
                        principalTable: "discount",
                        principalColumn: "discount_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "discount_menuitem",
                schema: "challenge",
                columns: table => new
                {
                    discount_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    menu_item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discount_menuitem", x => new { x.discount_id, x.menu_item_id });
                    table.ForeignKey(
                        name: "FK_discount_menuitem_discount_discount_id",
                        column: x => x.discount_id,
                        principalSchema: "challenge",
                        principalTable: "discount",
                        principalColumn: "discount_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_discount_menuitem_menu_item_menu_item_id",
                        column: x => x.menu_item_id,
                        principalSchema: "challenge",
                        principalTable: "menu_item",
                        principalColumn: "menu_item_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_item",
                schema: "challenge",
                columns: table => new
                {
                    order_item_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_item", x => x.order_item_id);
                    table.ForeignKey(
                        name: "FK_order_item_menu_item_MenuItemId",
                        column: x => x.MenuItemId,
                        principalSchema: "challenge",
                        principalTable: "menu_item",
                        principalColumn: "menu_item_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_item_order_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "challenge",
                        principalTable: "order",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_discount_menuitem_menu_item_id",
                schema: "challenge",
                table: "discount_menuitem",
                column: "menu_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_DiscountId",
                schema: "challenge",
                table: "order",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_MenuItemId",
                schema: "challenge",
                table: "order_item",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_OrderId",
                schema: "challenge",
                table: "order_item",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "discount_menuitem",
                schema: "challenge");

            migrationBuilder.DropTable(
                name: "order_item",
                schema: "challenge");

            migrationBuilder.DropTable(
                name: "user",
                schema: "challenge");

            migrationBuilder.DropTable(
                name: "menu_item",
                schema: "challenge");

            migrationBuilder.DropTable(
                name: "order",
                schema: "challenge");

            migrationBuilder.DropTable(
                name: "discount",
                schema: "challenge");
        }
    }
}
