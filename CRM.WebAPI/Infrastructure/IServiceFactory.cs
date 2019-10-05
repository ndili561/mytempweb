namespace CRM.WebAPI.Infrastructure
{
    public interface IServiceFactory<out T> where T : class
    {
        T Build();
    }
}
