using Aspirra.StudentService.Interfaces;
using Aspirra.StudentService.Models;

namespace Aspirra.StudentService.Services
{
    public class StudentProfileService : IStudentProfile
    {
        public Profile CreateProfile(string studentId, Profile data)
        {
            data.StudentId = studentId;
            return data;
        }

        public Profile UpdateProfile(string studentId, Profile data)
        {
            data.StudentId = studentId;
            return data;
        }

        public Profile GetProfile(string studentId)
        {
            return new Profile { StudentId = studentId };
        }

        public List<TimeSlot> GetAvailability(string studentId)
        {
            return new List<TimeSlot>();
        }
    }
}
