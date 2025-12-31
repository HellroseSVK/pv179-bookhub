using BusinessLayer.DTOs.Order;
using Mapster;
using MVC.Models.Order;

namespace MVC.Mapper;

public class MVCMappings : IRegister
{
    // Custom mapping belongs here
    public void Register(TypeAdapterConfig config)
    {
        // Order mappings
        config.NewConfig<OrderDTO, OrderViewModel>()
            .Map(dest => dest.UserEmail, src => src.User!.Email)
            .Map(dest => dest.Books, src => src.OrderItems.Select(oi => oi.Book));
    }
}