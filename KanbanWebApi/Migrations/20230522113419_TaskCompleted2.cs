using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanWebApi.Migrations
{
    /// <inheritdoc />
    public partial class TaskCompleted2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TaskCompleted",
                table: "KanbanTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskCompleted",
                table: "KanbanTasks");
        }
    }
}
