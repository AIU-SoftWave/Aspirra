using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aspirra.StudentService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "student");

            migrationBuilder.CreateTable(
                name: "Profiles",
                schema: "student",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AcademicLevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Major = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Interests = table.Column<string>(type: "text", nullable: true),
                    CareerInterests = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                schema: "student",
                columns: table => new
                {
                    TimeSlotId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StudentId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DayOfWeek = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.TimeSlotId);
                    table.ForeignKey(
                        name: "FK_TimeSlots_Profiles_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "student",
                        principalTable: "Profiles",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_StudentId",
                schema: "student",
                table: "TimeSlots",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSlots",
                schema: "student");

            migrationBuilder.DropTable(
                name: "Profiles",
                schema: "student");
        }
    }
}
