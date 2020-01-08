using System.Threading.Tasks;

namespace nopCommerceMobile.Services.RequestProvider
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri);
    }
}