namespace WebAPI.Models.User;

public record UserSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    string Email,
    string Password,
    bool IsAdmin
) : BaseResponse(Id, CreatedAt);