namespace WebAPI.Models.Response;

public abstract record BaseResponse(
    int Id,
    DateTime CreatedAt
);