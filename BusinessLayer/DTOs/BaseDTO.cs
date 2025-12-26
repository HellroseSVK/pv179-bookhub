namespace BusinessLayer.DTOs;

// Inherit this class if you want to have Id and CreatedAt in DTO
public abstract class BaseDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
};