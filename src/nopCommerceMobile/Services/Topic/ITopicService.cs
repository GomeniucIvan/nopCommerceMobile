using System.Threading.Tasks;
using nopCommerceMobile.Models.Topics;

namespace nopCommerceMobile.Services.Topic
{
    public interface ITopicService
    {
        Task<TopicModel> GetModelBySystemNameAsync(string systemName);
    }
}
