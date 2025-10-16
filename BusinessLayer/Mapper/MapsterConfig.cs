using BusinessLayer.DTOs.Simplified;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Mapper
{
    public static class MapsterConfig
    {
        // Custom mapping belongs here
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            TypeAdapterConfig<UserSimplifiedPartialDTO, UserSimplifiedDTO>
                .NewConfig()
                .IgnoreNullValues(true);
        }
    }
}
