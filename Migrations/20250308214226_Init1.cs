using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncStyle.Migrations
{
    /// <inheritdoc />
    public partial class Init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "contact_mechanism_id_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "style_sync_prod_id_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "user_id_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Password = table.Column<string>(type: "varchar(25)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<string>(type: "varchar(10)", nullable: false),
                    Role = table.Column<string>(type: "varchar(10)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FailedLoginAttempts = table.Column<int>(type: "integer", nullable: false),
                    LastFailedLoginAttempt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsLocked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ContactMechanism",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ContactMechanismInfo = table.Column<string>(type: "varchar(100)", nullable: false),
                    ContactMechanismType = table.Column<string>(type: "varchar(10)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMechanism", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactMechanism_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StyleSyncProd",
                columns: table => new
                {
                    StyleSyncProdId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(160000)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StyleSyncProd", x => x.StyleSyncProdId);
                    table.ForeignKey(
                        name: "FK_StyleSyncProd_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactMechanism_UserId",
                table: "ContactMechanism",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StyleSyncProd_UserId",
                table: "StyleSyncProd",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMechanism");

            migrationBuilder.DropTable(
                name: "StyleSyncProd");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropSequence(
                name: "contact_mechanism_id_hilo");

            migrationBuilder.DropSequence(
                name: "style_sync_prod_id_hilo");

            migrationBuilder.DropSequence(
                name: "user_id_hilo");
        }
    }
}
