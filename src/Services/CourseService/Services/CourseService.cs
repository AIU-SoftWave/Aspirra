using Aspirra.CourseService.Interfaces;
using Aspirra.CourseService.Models;

namespace Aspirra.CourseService.Services
{
    public class CourseService : ICourse
    {
        public Course AddCourse(Course course) => course;
        public Course UpdateCourse(string courseId, Course course) { course.CourseId = courseId; return course; }
        public void RemoveCourse(string courseId) { }
        public List<Course> GetCourses(string studentId) => new();
        public object GetCourseProgress(string courseId) => new();
    }
}
