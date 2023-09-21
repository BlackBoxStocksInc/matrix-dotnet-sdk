using System.Runtime.Serialization;

namespace BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Event
{
    public enum MessageType
    {
        [EnumMember(Value = "m.text")] Text
        // [JsonProperty("m.text")] Text
    }
}