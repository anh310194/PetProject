using System.Text.Json.Serialization;

namespace PetProject.WebAPI.Models
{
    public class MenuModel
    {
        [JsonPropertyName("route")]
        public string? Route { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("icon")]
        public string? Icon { get; set; }
        [JsonPropertyName("children")]
        public MenuChildrenItem[]? Children { get; set; }
    }

    public class MenuChildrenItem
    {
        [JsonPropertyName("route")]
        public string? Route { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("children")]
        public MenuChildrenItem[]? Children { get; set; }
    }
}
