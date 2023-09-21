using System.Runtime.Serialization;

namespace BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Room.Create
{
    public enum Preset
    {
        [EnumMember(Value = "private_chat")] PrivateChat,

        [EnumMember(Value = "public_chat")] PublicChat,

        [EnumMember(Value = "trusted_private_chat")]
        TrustedPrivateChat
    }
}