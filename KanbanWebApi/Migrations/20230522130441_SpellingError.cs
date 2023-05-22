using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanWebApi.Migrations
{
    /// <inheritdoc />
    public partial class SpellingError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanTasks_Members_AssingedId",
                table: "KanbanTasks");

            migrationBuilder.RenameColumn(
                name: "AssingedId",
                table: "KanbanTasks",
                newName: "AssignedId");

            migrationBuilder.RenameIndex(
                name: "IX_KanbanTasks_AssingedId",
                table: "KanbanTasks",
                newName: "IX_KanbanTasks_AssignedId");

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanTasks_Members_AssignedId",
                table: "KanbanTasks",
                column: "AssignedId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanTasks_Members_AssignedId",
                table: "KanbanTasks");

            migrationBuilder.RenameColumn(
                name: "AssignedId",
                table: "KanbanTasks",
                newName: "AssingedId");

            migrationBuilder.RenameIndex(
                name: "IX_KanbanTasks_AssignedId",
                table: "KanbanTasks",
                newName: "IX_KanbanTasks_AssingedId");

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanTasks_Members_AssingedId",
                table: "KanbanTasks",
                column: "AssingedId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
