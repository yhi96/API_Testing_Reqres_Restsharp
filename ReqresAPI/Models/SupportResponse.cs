using System.Text.Json.Serialization;

namespace ReqresAPI.Models
{
    public class SupportResponse
    {
        [JsonPropertyName("job")]
        public string LastCreatedUserJob { get; set; }

        [JsonPropertyName("name")]
        public string LastCreatedUserName { get; set; }

        [JsonPropertyName("id")]
        public string LastCreatedUserId { get; set; }

        [JsonPropertyName("updatedAt")]
        public string UpdatedAt { get; set; }
    }

}
