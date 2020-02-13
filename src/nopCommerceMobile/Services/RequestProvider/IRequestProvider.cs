using System.Threading.Tasks;

namespace nopCommerceMobile.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri);
        Task<TResult> PostAsync<TResult, TModel>(string uri, TModel data);
        Task<TResult> PostAsyncAnonymous<TResult, TModel>(string uri, TModel data);
        Task PostAsync<TModel>(string uri, TModel data);
    }
}