using Aspirra.SkillService.Common;

namespace Aspirra.SkillService.Exceptions;

public class SkillTransitionException : InvalidOperationException
{
    public SkillTransitionException(string message) : base(message)
    {
    }

    public string Code => SkillErrorCodes.InvalidTransition;
}
