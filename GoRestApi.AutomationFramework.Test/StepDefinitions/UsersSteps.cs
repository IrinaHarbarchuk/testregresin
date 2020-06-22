using GoRestApi.AutomationFramework.Test.ApiClient;
using GoRestApi.AutomationFramework.Test.ApiModels;
using GoRestApi.AutomationFramework.Test.ModelsForCreation;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace GoRestApi.AutomationFramework.Test.StepDefinitions
{
    [Binding]
    public sealed class UsersSteps
    {
        private readonly IRegresInApiClient _client;
        private readonly ScenarioContext context;

        public UsersSteps(ScenarioContext injectedContext, IRegresInApiClient client)
        {
            context = injectedContext;
            _client = client;
        }

        [Then(@"I get all users from (.*) page\(-s\) via reqres\.in api")]
        [When(@"I get all users from (.*) page\(-s\) via reqres\.in api")]
        public void WhenIGetAllUsersViaReqres_InApi(string page)
        {
            int requestPage = string.IsNullOrEmpty(page) ? 0 : (page.Equals("all", StringComparison.InvariantCultureIgnoreCase) ? 0 : int.Parse(page));
            var response = _client.GetUsers(requestPage);
            context.Add(ContextConstants.UsersListResponse, response);
            context[ContextConstants.StatusCode]= (int)response.StatusCode;

        }

        [Then(@"I see that (.*) status code was returned in response")]
        public void ThenISeeThatStatusCodeWasReturnedInResponse(int code)
        {
            var statusCode = context.Get<int>(ContextConstants.StatusCode);
            Assert.AreEqual(code, statusCode, $"Status code returned by api is {(int)statusCode}, but '{code}' is expected.");
        }

        [Then(@"I see that users list from (.*) page was returned in response")]
        public void ThenISeeThatUsersListWasReturnedInResponse(int page)
        {
            var response = context.Get<IRestResponse>(ContextConstants.UsersListResponse);
            var users = new JsonDeserializer().Deserialize<Users>(response);

            CollectionAssert.IsNotEmpty(users.data, "Users collection is empty, but not empty is expected.");
            Assert.AreEqual(page, users.page, "Users result page doesn't match expected one.");
        }

        [Given(@"I have created user data as follow")]
        public void GivenIHaveCreatedUserDataAsFollow(Table table)
        {
            var userData = table.CreateInstance<User>();
            context.Add(ContextConstants.UserData, userData);
        }

        [When(@"I create new user with data prepared via reqres\.in api")]
        public void WhenICreateNewUserWithDataPreparedViaReqres_InApi()
        {
            var user = context.Get<User>(ContextConstants.UserData);
            var response = _client.PostUser(user);
            context.Add(ContextConstants.StatusCode, (int)response.StatusCode);
            context.Add(ContextConstants.UserPostResponse, response);
        }

        [Then(@"I see that created user is returned in users list")]
        public void ThenISeeThatCreatedUserIsReturnedInUsersList()
        {
            var response = context.Get<IRestResponse>(ContextConstants.UsersListResponse);
            var users = new JsonDeserializer().Deserialize<Users>(response).data;

            CollectionAssert.IsNotEmpty(users, "Users collection is empty, but not empty is expected.");
            var expectedUser = context.Get<User>(ContextConstants.UserData);
            var foundUser = users.FirstOrDefault(u=>u.first_name.Equals(expectedUser.name));
            Assert.IsNotNull(foundUser, "Created user wasn't found on returned list.");
        }

        [Then(@"I get user with id of created user via reqres\.in api")]
        public void ThenIGetUserWithIdOfCreatedUserViaReqres_InApi()
        {
            var postResponse = context.Get<IRestResponse>(ContextConstants.UserPostResponse);
            var retrnedUser = new JsonDeserializer().Deserialize<PostUserResponse>(postResponse);
            var getUserResponse = _client.GetUser(retrnedUser.id);
            context.Add(ContextConstants.UserGetResponse, getUserResponse);
        }

        [Then(@"I see that created user matches created one")]
        public void ThenISeeThatCreatedUserMatchesCreatedOne()
        {
            var response = context.Get<IRestResponse>(ContextConstants.UserGetResponse);
            var gotUser = new JsonDeserializer().Deserialize<GetUserResponse>(response).data;
            Assert.IsNotNull(gotUser, "User was not found, null. But non null user object is expected.");
           
            var expectedUser = context.Get<User>(ContextConstants.UserData);
            Assert.AreEqual(expectedUser.name, gotUser.first_name, "");
      
        }







    }
}
