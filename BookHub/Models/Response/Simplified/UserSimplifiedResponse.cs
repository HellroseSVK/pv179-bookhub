namespace WebAPI.Models.Response.Simplified;

public record UserSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    string Email,
    string Password,
    bool IsAdmin
) : BaseResponse(Id, CreatedAt);