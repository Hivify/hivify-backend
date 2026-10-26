
namespace Identity.Application.DTOs;

public sealed record ConfirmEmailResult(
    bool Succeeded,
    IReadOnlyList<string> Errors);
