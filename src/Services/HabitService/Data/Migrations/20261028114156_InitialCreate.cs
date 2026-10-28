using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aspirra.HabitService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "habit");

            migrationBuilder.CreateTable(
                name: "Habits",
                schema: "habit",
                columns: table => new
                {
                    HabitId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    StudentId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    GoalId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Frequency = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    GoalTarget = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ReminderTime = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    CurrentStreak = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habits", x => x.HabitId);
                });

            migrationBuilder.CreateTable(
                name: "HabitLogs",
                schema: "habit",
                columns: table => new
                {
                    LogId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    HabitId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    LogDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Completed = table.Column<bool>(type: "boolean", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HabitLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_HabitLogs_Habits_HabitId",
                        column: x => x.HabitId,
                        principalSchema: "habit",
                        principalTable: "Habits",
                        principalColumn: "HabitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HabitLogs_HabitId_LogDate",
                schema: "habit",
                table: "HabitLogs",
                columns: new[] { "HabitId", "LogDate" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HabitLogs",
                schema: "habit");

            migrationBuilder.DropTable(
                name: "Habits",
                schema: "habit");
        }
    }
}
