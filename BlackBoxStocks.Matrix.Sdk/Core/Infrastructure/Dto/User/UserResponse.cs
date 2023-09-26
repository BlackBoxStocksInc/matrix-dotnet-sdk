using Newtonsoft.Json;

namespace BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.User
{
    public class UserResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("displayname")] 
        public string DisplayName { get; set; } = string.Empty;
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; } = string.Empty;
    }
}
