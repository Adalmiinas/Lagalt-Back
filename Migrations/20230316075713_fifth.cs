using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lagalt.Migrations
{
    /// <inheritdoc />
    public partial class fifth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_searchWords_Users_userId",
                table: "searchWords");

            migrationBuilder.DropPrimaryKey(
                name: "PK_searchWords",
                table: "searchWords");

            migrationBuilder.RenameTable(
                name: "searchWords",
                newName: "SearchWordModel");

            migrationBuilder.RenameIndex(
                name: "IX_searchWords_userId",
                table: "SearchWordModel",
                newName: "IX_SearchWordModel_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SearchWordModel",
                table: "SearchWordModel",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AppliedProjectHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UserModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedProjectHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppliedProjectHistories_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppliedProjectHistories_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ClickedProjectHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    UserModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClickedProjectHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClickedProjectHistories_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClickedProjectHistories_Users_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppliedProjectHistories_ProjectId",
                table: "AppliedProjectHistories",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AppliedProjectHistories_UserModelId",
                table: "AppliedProjectHistories",
                column: "UserModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ClickedProjectHistories_ProjectId",
                table: "ClickedProjectHistories",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ClickedProjectHistories_UserModelId",
                table: "ClickedProjectHistories",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_SearchWordModel_Users_userId",
                table: "SearchWordModel",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SearchWordModel_Users_userId",
                table: "SearchWordModel");

            migrationBuilder.DropTable(
                name: "AppliedProjectHistories");

            migrationBuilder.DropTable(
                name: "ClickedProjectHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SearchWordModel",
                table: "SearchWordModel");

            migrationBuilder.RenameTable(
                name: "SearchWordModel",
                newName: "searchWords");

            migrationBuilder.RenameIndex(
                name: "IX_SearchWordModel_userId",
                table: "searchWords",
                newName: "IX_searchWords_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_searchWords",
                table: "searchWords",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_searchWords_Users_userId",
                table: "searchWords",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
