namespace Aspirra.AcademicService.Exceptions
{
    public class GoalNotFoundException : Exception
    {
        public GoalNotFoundException() : base("Goal not found")
        {
        }
    }
}
