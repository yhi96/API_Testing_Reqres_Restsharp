using ReqresAPI.Models;
using RestSharp;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
namespace ReqresAPI
{
    public class ReqresAPItest
    {
        private RestClient client;
        public string token = "";
        [SetUp]
        public void SetUp()
        {
            client = new RestClient("https://reqres.in/");

        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
        }

        [Test]
        public void Test_Post_Register_User_With_Given_Data()
        {
            var newUser = new UserRequest()
            {
                Email = "eve.holt@reqres.in",
                Password = "pistol",
            };

            var request = new RestRequest("api/register", Method.Post);
            request.AddJsonBody(newUser);
           
            var response = client.Execute(request);
            var responseData = JsonSerializer.Deserialize<UserResponse>(response.Content);
            token = responseData.token;

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseData.token, Is.Not.Empty);
            Assert.That(responseData.Id, Is.EqualTo(4));

        }

        [Test]
        public void Test_Post_Register_User_Without_Password()
        {
            var newUser = new UserRequest()
            {
                Email = "peter@klaven",
                Password = ""
            };
            var request = new RestRequest("api/login", Method.Post);
            request.AddJsonBody(newUser);
            var response = client.Execute(request);
            var responseData = JsonSerializer.Deserialize<UserResponse>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest)); 

        }

        [Test]
        public void Test_Post_Login_User_With_Given_Data()
        {
            var newUser = new UserRequest()
            {
                Email = "eve.holt@reqres.in",
                Password = "cityslicka"
            };
            var request = new RestRequest("api/login", Method.Post);
            request.AddJsonBody(newUser);
            var response = client.Execute(request);
            var responseData = JsonSerializer.Deserialize<UserResponse>(response.Content);

    
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseData.token, Is.EqualTo("QpwL5tke4Pnpja7X4"));
        }

        [Test]
        public void Test_Post_Login_User_Without_Password()
        {
            var newUser = new UserRequest()
            {
                Email = "eve.holt@reqres.in",
                Password = ""
            };
            var request = new RestRequest("api/login", Method.Post);
            request.AddJsonBody(newUser);
            var response = client.Execute(request);
            var responseData = JsonSerializer.Deserialize<UserResponse>(response.Content);


            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
            Assert.That(responseData.error, Is.EqualTo("Missing password"));
        }

        [Test]
        public void Test_Post_Create_User_With_Given_Data()
        {
            var newUser = new UserRequest()
            {
                Name = "morpheus",
                Job = "leader"
            };

            var request = new RestRequest("api/users", Method.Post);
            request.AddJsonBody(newUser);
            var response = client.Execute(request);
            var responseData = JsonSerializer.Deserialize<SupportResponse>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(responseData.LastCreatedUserName, Is.EqualTo("morpheus"));
            Assert.That(responseData.LastCreatedUserJob, Is.EqualTo("leader"));
            Assert.That(responseData.LastCreatedUserId, Is.Not.Empty);
            Assert.That(responseData.CreatedAt, Is.Not.Empty);
        }

        [Test]
        public void Test_Put_Update_User_With_Given_Data()
        {
            var upDateUser = new UserRequest()
            {
                Name = "morpheus",
                Job = "zion resident"
            };

            var request = new RestRequest("api/users/2", Method.Put);
            request.AddJsonBody(upDateUser);
            var response = client.Execute(request);
            var responseData = JsonSerializer.Deserialize<SupportResponse>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseData.LastCreatedUserName, Is.EqualTo("morpheus"));
            Assert.That(responseData.LastCreatedUserJob, Is.EqualTo("zion resident"));
            Assert.That(responseData.LastCreatedUserId, Is.Not.Empty);
            Assert.That(responseData.UpdatedAt, Is.Not.Empty);

            DateTime updatedAt;
            bool isDateValid = DateTime.TryParse(responseData.UpdatedAt, out updatedAt);
            Assert.That(isDateValid, Is.True, "UpdatedAt is not a valid date format");

            var timeDifference = DateTime.UtcNow - updatedAt;
            Assert.That(timeDifference.TotalSeconds, Is.LessThan(10), "UpdatedAt timestamp is not within the expected time range");
        

    }

        [Test]
        public void Test_Get_List_Of_All_Users()
        {
            var request = new RestRequest("https://reqres.in/api/users?page=2", Method.Get);
       
            var response = client.Execute(request);
            var responseData = JsonSerializer.Deserialize<GetListUsersResponse>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var countUsers = responseData.Data.Count;
            Assert.That(countUsers, Is.EqualTo(6));
            Assert.That(responseData.Page, Is.EqualTo(2));
            Assert.That(responseData.PerPage, Is.EqualTo(6));
            Assert.That(responseData.TotalPages, Is.EqualTo(2));
             
        }

        [Test]
        public void Test_Get_List_Of_Single_User()
        {
            var request = new RestRequest("api/users/2", Method.Get);

            var response = client.Execute(request);
            var responseData = JsonSerializer.Deserialize<GetSingleUserResponse>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseData.Data.Id, Is.EqualTo(2));
        }

        [Test]
        public void Test_Get_List_Of_User_That_Does_Not_Exist()
        {
            var request = new RestRequest("api/users/23", Method.Get);

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public void Test_Get_List_Of_Existing_Colors()
        {
            var request = new RestRequest("api/unknown", Method.Get);

            var response = client.Execute(request);
            var responseData = JsonSerializer.Deserialize<AllColorsResponse>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var colorsCount = responseData.Colors.Count;

            Assert.That(colorsCount, Is.EqualTo(6));
        }

        [Test]
        public void Test_Get_List_Of_Single_Color()
        {
            var request = new RestRequest("api/unknown/2", Method.Get);

            var response = client.Execute(request);
            var responseData = JsonSerializer.Deserialize<SingleColor>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var colorId = responseData.Data.Id;

            Assert.That(colorId, Is.EqualTo(2));
        }

        [Test]
        public void Test_Get_List_Of_Single_Color_That_Does_Not_Exist()
        {
            var request = new RestRequest("api/unknown/23", Method.Get);

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            
        }

        [Test]
        public void Test_Get_List_Of_Users_With_Delayed_Response()
        {
            var request = new RestRequest("api/users/2", Method.Get);

            Thread.Sleep(2000); 


            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        }

        [Test]
        public void Test_Delete_User()
        {
            var request = new RestRequest("api/users/2", Method.Get);

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

    }
}