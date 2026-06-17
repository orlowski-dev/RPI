using System.Runtime.CompilerServices;

namespace Game.Core.Application.Results;

// np new Error(
//    "combat.no_action",
//    "No action selected",
//    ErrorType.Validation)
public record Error(string Message, ErrorType Type, [CallerMemberName] string Code = "");
