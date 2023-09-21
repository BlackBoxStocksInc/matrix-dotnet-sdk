using System.Runtime.Serialization;

namespace BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Room.Create
{
    public enum Visibility
    {
        [EnumMember(Value = "public")] Public,

        [EnumMember(Value = "private")] Private
    }
}