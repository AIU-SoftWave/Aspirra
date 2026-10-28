namespace Aspirra.AcademicService.Exceptions
{
    public class GoalValidationException : Exception
    {
        public GoalValidationException(string code, string message) : base(message)
        {
            Code = code;
        }

        public string Code { get; }
    }
}
