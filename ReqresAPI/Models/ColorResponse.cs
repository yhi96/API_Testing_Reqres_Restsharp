using System.Text.Json.Serialization;

namespace ReqresAPI.Models
{
    public class AllColorsResponse
    {
        [JsonPropertyName("data")]
        public List<ColorData> Colors { get; set; }
    }

    public class ColorData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("color")]
        public string ColorHex { get; set; }

        [JsonPropertyName("pantone_value")]
        public string PantoneValue { get; set; }
    }

    public class SingleColor
    {
        [JsonPropertyName("data")]
        public ColorData Data { get; set; }
    }


}
