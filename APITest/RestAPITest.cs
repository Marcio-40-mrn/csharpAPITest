using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;

namespace APITest
{
    public class RestAPITest
    {
        protected ExtentReports extent;
        protected ExtentKlovReporter klov;
        protected ExtentTest test;

        [SetUp]
        public void Setup()
        {
            var htmlReport = new ExtentHtmlReporter(@"C:\Users\marcio.nascimento\source\repos\API\APITest\relatorios\relatorio.html");
            htmlReport.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent = new ExtentReports();
            klov = new ExtentKlovReporter();
            extent.AttachReporter(htmlReport);
        }

        [Test]
        public void PostCreateUser()
        {
            test = extent.CreateTest("PostCreateUser");
            string jsonString = @"{
                                    ""name"": ""morpheus"",
                                    ""job"": ""leader""
                                }";
            RestApiHelper<CreateUser> restApi = new RestApiHelper<CreateUser>();
            var restUrl = restApi.SetUrl("api/users");
            var restRequest = restApi.CreatePostRequest(jsonString);
            var response = restApi.GetResponse(restUrl, restRequest);
            CreateUser content = restApi.GetContent<CreateUser>(response);

            if (content.name == "morpheus")
            {
                if(content.job == "leader")
                {
                    test.Log(Status.Pass, "Json de Resposta OK: " + response.Content);
                    extent.Flush();
                    Assert.Pass();

                }
                else
                {
                    test.Log(Status.Fail, "Json de Resposta NOK: " + response.Content);
                    extent.Flush();
                    Assert.Fail();
                }
                
            }
            else
            {
                test.Log(Status.Fail, "Json de Resposta NOK: " + response.Content);
                extent.Flush();
                Assert.Fail();
            }
        }

              
    }
}