using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aspirra.GoalPlannerService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "goal_planner");

            migrationBuilder.CreateTable(
                name: "PlanDrafts",
                schema: "goal_planner",
                columns: table => new
                {
                    DraftId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    StudentId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    GoalId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    GoalText = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanDrafts", x => x.DraftId);
                });

            migrationBuilder.CreateTable(
                name: "Milestones",
                schema: "goal_planner",
                columns: table => new
                {
                    MilestoneId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    DraftId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Deadline = table.Column<DateOnly>(type: "date", nullable: false),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Milestones", x => x.MilestoneId);
                    table.ForeignKey(
                        name: "FK_Milestones_PlanDrafts_DraftId",
                        column: x => x.DraftId,
                        principalSchema: "goal_planner",
                        principalTable: "PlanDrafts",
                        principalColumn: "DraftId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanTasks",
                schema: "goal_planner",
                columns: table => new
                {
                    TaskId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    MilestoneId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    DurationHours = table.Column<int>(type: "integer", nullable: false),
                    Deadline = table.Column<DateOnly>(type: "date", nullable: false),
                    DependsOnTaskId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    OrderIndex = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanTasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_PlanTasks_Milestones_MilestoneId",
                        column: x => x.MilestoneId,
                        principalSchema: "goal_planner",
                        principalTable: "Milestones",
                        principalColumn: "MilestoneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Milestones_DraftId",
                schema: "goal_planner",
                table: "Milestones",
                column: "DraftId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanTasks_MilestoneId",
                schema: "goal_planner",
                table: "PlanTasks",
                column: "MilestoneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanTasks",
                schema: "goal_planner");

            migrationBuilder.DropTable(
                name: "Milestones",
                schema: "goal_planner");

            migrationBuilder.DropTable(
                name: "PlanDrafts",
                schema: "goal_planner");
        }
    }
}
