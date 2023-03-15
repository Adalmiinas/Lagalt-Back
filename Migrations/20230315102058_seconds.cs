using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lagalt.Migrations
{
    /// <inheritdoc />
    public partial class seconds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageBoards_Projects_ProjectModelId",
                table: "MessageBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInWaitingLists_Users_UserId",
                table: "UsersInWaitingLists");

            migrationBuilder.DropIndex(
                name: "IX_MessageBoards_ProjectModelId",
                table: "MessageBoards");

            migrationBuilder.DropColumn(
                name: "ProjectModelId",
                table: "MessageBoards");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UsersInWaitingLists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotivationLetter",
                table: "UsersInWaitingLists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoards_ProjectId",
                table: "MessageBoards",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageBoards_Projects_ProjectId",
                table: "MessageBoards",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInWaitingLists_Users_UserId",
                table: "UsersInWaitingLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageBoards_Projects_ProjectId",
                table: "MessageBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInWaitingLists_Users_UserId",
                table: "UsersInWaitingLists");

            migrationBuilder.DropIndex(
                name: "IX_MessageBoards_ProjectId",
                table: "MessageBoards");

            migrationBuilder.DropColumn(
                name: "MotivationLetter",
                table: "UsersInWaitingLists");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UsersInWaitingLists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProjectModelId",
                table: "MessageBoards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoards_ProjectModelId",
                table: "MessageBoards",
                column: "ProjectModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageBoards_Projects_ProjectModelId",
                table: "MessageBoards",
                column: "ProjectModelId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInWaitingLists_Users_UserId",
                table: "UsersInWaitingLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
