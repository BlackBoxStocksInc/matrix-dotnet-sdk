using Newtonsoft.Json;

namespace BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Sync.Event.Room.Messaging
{
    public enum MessageType
    {
        Text,
        Image,
        Unknown
    }

    public record ImageContent
    {
        public int H { get; set; }
        [JsonProperty("mimetype")]
        public string MimeType { get; set; }
        public long Size { get; set; }
        public int W { get; set; }
    }

    public record MessageContent
    {
        /// <summary>
        ///     <b>Required.</b> The textual representation of this message.
        /// </summary>
        public string Body { get; init; }

        [JsonProperty("info")]
        public ImageContent ImageContent { get; set; }
        public string Url { get; set; }

        public MessageType MessageType { get; private set; }

        /// <summary>
        ///     <b>Required.</b> The type of message, e.g. m.image, m.text
        /// </summary>
        [JsonProperty("msgtype")]
        public string Type
        {
            set => MessageType = value switch
            {
                Constants.MessageType.Text => MessageType.Text,
                Constants.MessageType.Image => MessageType.Image,
                _ => MessageType.Unknown
            };
        }
    }
}