using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanWebApi.Migrations
{
    /// <inheritdoc />
    public partial class TaskCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompletedById",
                table: "KanbanTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KanbanTasks_CompletedById",
                table: "KanbanTasks",
                column: "CompletedById");

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanTasks_Members_CompletedById",
                table: "KanbanTasks",
                column: "CompletedById",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanTasks_Members_CompletedById",
                table: "KanbanTasks");

            migrationBuilder.DropIndex(
                name: "IX_KanbanTasks_CompletedById",
                table: "KanbanTasks");

            migrationBuilder.DropColumn(
                name: "CompletedById",
                table: "KanbanTasks");
        }
    }
}
