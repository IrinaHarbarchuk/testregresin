using GoRestApi.AutomationFramework.Test.ApiModels;
using GoRestApi.AutomationFramework.Test.ModelsForCreation;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Configuration;

namespace GoRestApi.AutomationFramework.Test.ApiClient
{
    public class RegresInApiClient : IRegresInApiClient
    {
        public readonly RestClient Client;

        public RegresInApiClient()
        {
            Client = Client ?? new RestClient();
            Client.BaseUrl = new Uri(ConfigurationManager.AppSettings.Get("RegresInBaseUrl"));
        }

        public IRestResponse GetUser(string userId)
        {
            IRestRequest request = new RestRequest($"users/{userId}");
            var response = Client.Get(request);
            return response;

        }

        public IRestResponse GetUsers(int page)
        {
            string resourse = page==default(int) ? $"users" : $"users?page={page}";
            IRestRequest req = new RestRequest(resourse);
            var response = Client.Get(req);
            return response;
        }

        public IRestResponse PostUser(User user)
        {
            IRestRequest req = new RestRequest("users");
            req.AddJsonBody(user);
            var response = Client.Post(req);
            return response;
        }  
    }
}
