using Mapster;

namespace WebAPI.Configurations;

public static class MapsterConfig
{
    public static void RegisterMapsterConfiguration(this IServiceCollection services)
    {
        // TypeAdapterConfig.GlobalSettings
        //     .NewConfig()
        //     .IgnoreNullValues(true);
    }
    
}