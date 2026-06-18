using System.Runtime.CompilerServices;

// np new Error(
//    "combat.no_action",
//    "No action selected",
//    ErrorType.Validation)
public record Error(string Message, ErrorType Type, [CallerMemberName] string Code = "");
