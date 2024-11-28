using MarketplaceService.APIs;

namespace MarketplaceService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICartsService, CartsService>();
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<IListingsService, ListingsService>();
        services.AddScoped<IMaterialsService, MaterialsService>();
        services.AddScoped<IPaymentsService, PaymentsService>();
        services.AddScoped<ISearchesService, SearchesService>();
        services.AddScoped<ITechnologiesService, TechnologiesService>();
        services.AddScoped<IWorkersService, WorkersService>();
    }
}
