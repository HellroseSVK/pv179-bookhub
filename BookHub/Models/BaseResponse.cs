namespace WebAPI.Models;

public abstract record BaseResponse(
    int Id,
    DateTime CreatedAt
);