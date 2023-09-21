using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Sync.Event;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Sync.Event.Room;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Sync.Event.Room.State;

namespace BlackBoxStocks.Matrix.Sdk.Core.Domain.RoomEvent
{
    public record CreateRoomEvent(string RoomId, string SenderUserId, string RoomCreatorUserId) : BaseRoomEvent(RoomId,
        SenderUserId)
    {
        public static class Factory
        {
            public static bool TryCreateFrom(Infrastructure.Dto.Sync.Event.Room.RoomEvent roomEvent, string roomId, out CreateRoomEvent createRoomEvent)
            {
                RoomCreateContent content = roomEvent.Content.ToObject<RoomCreateContent>();
                if (roomEvent.EventType == EventType.Create && content != null)
                {
                    createRoomEvent = new CreateRoomEvent(roomId, roomEvent.SenderUserId, content.RoomCreatorUserId);
                    return true;
                }

                createRoomEvent = new CreateRoomEvent(string.Empty, string.Empty, string.Empty);
                return false;
            }

            public static bool TryCreateFromStrippedState(RoomStrippedState roomStrippedState, string roomId,
                out CreateRoomEvent createRoomEvent)
            {
                RoomCreateContent content = roomStrippedState.Content.ToObject<RoomCreateContent>();
                if (roomStrippedState.EventType == EventType.Create && content != null)
                {
                    createRoomEvent =
                        new CreateRoomEvent(roomId, roomStrippedState.SenderUserId, content.RoomCreatorUserId);
                    return true;
                }

                createRoomEvent = new CreateRoomEvent(string.Empty, string.Empty, string.Empty);
                return false;
            }
        }
    }
}