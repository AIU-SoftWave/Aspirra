using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aspirra.CourseService.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "course");

            migrationBuilder.CreateTable(
                name: "Courses",
                schema: "course",
                columns: table => new
                {
                    CourseId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    StudentId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Instructor = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Semester = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Credits = table.Column<int>(type: "integer", nullable: false),
                    WeeklyWorkloadHours = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "CourseEnrollments",
                schema: "course",
                columns: table => new
                {
                    EnrollmentId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    CourseId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    StudentId = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    EnrollmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEnrollments", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_CourseEnrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "course",
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEnrollments_CourseId",
                schema: "course",
                table: "CourseEnrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Code_Semester",
                schema: "course",
                table: "Courses",
                columns: new[] { "Code", "Semester" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseEnrollments",
                schema: "course");

            migrationBuilder.DropTable(
                name: "Courses",
                schema: "course");
        }
    }
}
