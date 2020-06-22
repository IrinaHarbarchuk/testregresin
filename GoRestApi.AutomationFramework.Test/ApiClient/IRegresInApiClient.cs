using GoRestApi.AutomationFramework.Test.ApiModels;
using GoRestApi.AutomationFramework.Test.ModelsForCreation;
using RestSharp;

namespace GoRestApi.AutomationFramework.Test.ApiClient
{
    public interface IRegresInApiClient
    {
       IRestResponse GetUsers(int page);
       IRestResponse PostUser(User user);
        IRestResponse GetUser(string userId);

    }
}
