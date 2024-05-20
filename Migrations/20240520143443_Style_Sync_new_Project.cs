using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncStyle.Migrations
{
    /// <inheritdoc />
    public partial class Style_Sync_new_Project : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "member_id_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "style_sync_prod_id_hilo",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(25)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<string>(type: "varchar(50)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "StyleSyncProd",
                columns: table => new
                {
                    StyleSyncProdId = table.Column<int>(type: "integer", nullable: false),
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(4000)", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StyleSyncProd", x => x.StyleSyncProdId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "StyleSyncProd");

            migrationBuilder.DropSequence(
                name: "member_id_hilo");

            migrationBuilder.DropSequence(
                name: "style_sync_prod_id_hilo");
        }
    }
}
