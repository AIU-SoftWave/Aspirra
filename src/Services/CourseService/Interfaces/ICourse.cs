using Aspirra.CourseService.Models;

namespace Aspirra.CourseService.Interfaces
{
    public interface ICourse
    {
        Course AddCourse(Course course);
        Course UpdateCourse(string courseId, Course course);
        void RemoveCourse(string courseId);
        List<Course> GetCourses(string studentId);
        object GetCourseProgress(string courseId);
    }
}
