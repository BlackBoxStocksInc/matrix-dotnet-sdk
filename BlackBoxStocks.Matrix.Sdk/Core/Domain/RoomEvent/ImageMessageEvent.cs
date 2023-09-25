using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Sync.Event.Room.Messaging;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Sync.Event.Room;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Sync.Event;

namespace BlackBoxStocks.Matrix.Sdk.Core.Domain.RoomEvent
{
    public record ImageMessageEvent (string RoomId, string SenderId, string Body, ImageContent? ImageContent, string Url) : BaseRoomEvent(RoomId, SenderId)
    {
        public class Factory
        {
            public static bool TryCreateFrom(Infrastructure.Dto.Sync.Event.Room.RoomEvent roomEvent, string roomId, out ImageMessageEvent textMessageEvent)
            {
                var content = roomEvent.Content.ToObject<MessageContent>();
                if (roomEvent.EventType == EventType.Message && content?.MessageType == MessageType.Image)
                {
                    textMessageEvent = new ImageMessageEvent(roomId, roomEvent.SenderUserId, content.Body, content.ImageContent, content.Url);
                    return true;
                }

                textMessageEvent = new ImageMessageEvent(string.Empty, string.Empty, string.Empty, null, string.Empty);
                return false;
            }

            public static bool TryCreateFromStrippedState(RoomStrippedState roomStrippedState, string roomId,
                out ImageMessageEvent textMessageEvent)
            {
                var content = roomStrippedState.Content.ToObject<MessageContent>();
                if (roomStrippedState.EventType == EventType.Message && content?.MessageType == MessageType.Text)
                {
                    textMessageEvent = new ImageMessageEvent(roomId, roomStrippedState.SenderUserId, content.Body, content.ImageContent, content.Url);
                    return true;
                }

                textMessageEvent = new ImageMessageEvent(string.Empty, string.Empty, string.Empty, null, string.Empty);
                return false;
            }
        }
    }
}
