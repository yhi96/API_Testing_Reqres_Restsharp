using System.Text.Json.Serialization;

namespace ReqresAPI.Models
{
    public class UserResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("email")]
        public string UserEmail { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("token")]
        public string token { get; set; }

        [JsonPropertyName("error")]
        public string error { get; set; }
        [JsonPropertyName("job")]
        public string Job { get; set; }
       
     
    }
}
