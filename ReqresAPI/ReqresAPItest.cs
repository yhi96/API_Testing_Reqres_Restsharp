using ReqresAPI.Models;
using RestSharp;
using System.Net;
namespace ReqresAPI
{
    public class ReqresAPItest
    {
        private RestClient client;

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

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        }

        [Test]
        public void Test_Post_Register_User_Without_Password()
        {

        }

        [Test]
        public void Test_Post_Login_User_With_Given_Data()
        {

        }

        [Test]
        public void Test_Post_Login_User_Without_Password()
        {

        }

        [Test]
        public void Test_Post_Create_User_With_Given_Data()
        {

        }

        [Test]
        public void Test_Put_Update_User_With_Given_Data()
        {

        }

        [Test]
        public void Test_Get_List_Of_All_Users()
        {

        }

        [Test]
        public void Test_Get_List_Of_Single_User()
        {

        }

        [Test]
        public void Test_Get_List_Of_User_That_Does_Not_Exist()
        {

        }

        [Test]
        public void Test_Get_List_Of_Existing_Colors()
        {

        }

        [Test]
        public void Test_Get_List_Of_Single_Color()
        {

        }

        [Test]
        public void Test_Get_List_Of_Single_Color_That_Does_Not_Exist()
        {

        }

        [Test]
        public void Test_Get_List_Of_Users_With_Delayed_Response()
        {
           
        }

        [Test]
        public void Test_Delete_User()
        {

        }

    }
}