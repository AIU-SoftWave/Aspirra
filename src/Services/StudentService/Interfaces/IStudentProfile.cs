using Aspirra.StudentService.Models;

namespace Aspirra.StudentService.Interfaces
{
    public interface IStudentProfile
    {
        Profile CreateProfile(string studentId, Profile data);
        Profile UpdateProfile(string studentId, Profile data);
        Profile GetProfile(string studentId);
        List<TimeSlot> GetAvailability(string studentId);
    }
}
