namespace Movie_API.Interfaces
{
    public interface IModule
    {
        IServiceCollection RegisterModule(IServiceCollection services);

        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
    }
}
