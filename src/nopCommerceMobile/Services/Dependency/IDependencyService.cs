namespace nopCommerceMobile.Services.Dependency
{
    public interface IDependencyService
    {
        T Get<T>() where T : class;
    }
}
