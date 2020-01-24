using nopCommerceMobile.Models.Customer;
using System.Threading.Tasks;

namespace nopCommerceMobile.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri);
        Task<TResult> PostAsync<TResult, TModel>(string uri, TModel data);
    }
}