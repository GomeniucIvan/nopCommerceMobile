using System.Threading.Tasks;
using nopCommerceMobile.Models.Topics;
using nopCommerceMobile.Services.RequestProvider;

namespace nopCommerceMobile.Services.Topic
{
    public class TopicService : ITopicService
    {
        private static readonly string ApiUrlBase = $"{GlobalSettings.DefaultEndpoint}/api/topic";

        private readonly IRequestProvider _requestProvider;

        public TopicService(IRequestProvider requestProvider)
        {
            _requestProvider = requestProvider;
        }

        public async Task<TopicModel> GetModelBySystemNameAsync(string systemName)
        {
            var uri = $"{ApiUrlBase}/{systemName}";

            var topicModel = await _requestProvider.GetAsync<TopicModel>(uri);

            if (topicModel != null)
                return topicModel;

            return new TopicModel();
        }
    }
}
