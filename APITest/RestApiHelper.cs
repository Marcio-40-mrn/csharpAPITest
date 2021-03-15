using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
namespace APITest
{
    public class RestApiHelper<T>
    {

        public RestClient restClient;
        public RestRequest restRequest;
        public string baseUrl = "https://reqres.in/";

        public RestClient SetUrl(string endPoint)
        {
            var url = baseUrl + endPoint;
            var restClient = new RestClient(url);
            return restClient;
        }

        public RestRequest CreatePostRequest(string jsonString)
        {
            restRequest = new RestRequest(Method.POST);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return restRequest;
        }

        public RestRequest CreatePutRequest(string jsonString)
        {
            restRequest = new RestRequest(Method.PUT);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddParameter("application/json", jsonString, ParameterType.RequestBody);
            return restRequest;
        }

        public RestRequest CreateGetRequest()
        {
            restRequest = new RestRequest(Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }

        public RestRequest CreateDeleteRequest()
        {
            restRequest = new RestRequest(Method.DELETE);
            restRequest.AddHeader("Accept", "application/json");
            return restRequest;
        }

        public IRestResponse GetResponse(RestClient restClient, RestRequest restRequest)
        {
            return restClient.Execute(restRequest);
        }

        public DTO GetContent<DTO>(IRestResponse response)
        {
            var content = response.Content;
            DTO deseiralizeObject = Newtonsoft.Json.JsonConvert.DeserializeObject<DTO>(content);
            return deseiralizeObject;
        }
    }
}
