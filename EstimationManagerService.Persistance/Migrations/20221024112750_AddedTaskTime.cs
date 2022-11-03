using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstimationManagerService.Persistance.Migrations
{
    public partial class AddedTaskTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStarted",
                table: "UserTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "TaskEndDate",
                table: "UserTasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TaskEstimationTime",
                table: "UserTasks",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TaskStartDate",
                table: "UserTasks",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TaskTimeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTimeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTimeDetails_UserTasks_UserTaskId",
                        column: x => x.UserTaskId,
                        principalTable: "UserTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskTimeDetails_UserTaskId",
                table: "TaskTimeDetails",
                column: "UserTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskTimeDetails");

            migrationBuilder.DropColumn(
                name: "IsStarted",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "TaskEndDate",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "TaskEstimationTime",
                table: "UserTasks");

            migrationBuilder.DropColumn(
                name: "TaskStartDate",
                table: "UserTasks");
        }
    }
}
