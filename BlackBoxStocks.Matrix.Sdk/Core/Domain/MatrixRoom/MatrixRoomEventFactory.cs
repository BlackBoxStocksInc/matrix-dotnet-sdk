using System.Collections.Generic;
using BlackBoxStocks.Matrix.Sdk.Core.Domain.RoomEvent;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Sync;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Sync.Event.Room;

namespace BlackBoxStocks.Matrix.Sdk.Core.Domain.MatrixRoom
{
    public class MatrixRoomEventFactory
    {
        public List<BaseRoomEvent> CreateFromJoined(string roomId, JoinedRoom joinedRoom)
        {
            var roomEvents = new List<BaseRoomEvent>();

            foreach (Infrastructure.Dto.Sync.Event.Room.RoomEvent timelineEvent in joinedRoom.Timeline.Events)
                if (JoinRoomEvent.Factory.TryCreateFrom(timelineEvent, roomId, out JoinRoomEvent joinRoomEvent))
                    roomEvents.Add(joinRoomEvent!);
                else if (CreateRoomEvent.Factory.TryCreateFrom(timelineEvent, roomId,
                             out CreateRoomEvent createRoomEvent))
                    roomEvents.Add(createRoomEvent!);
                else if (InviteToRoomEvent.Factory.TryCreateFrom(timelineEvent, roomId,
                             out InviteToRoomEvent inviteToRoomEvent))
                    roomEvents.Add(inviteToRoomEvent!);
                else if (TextMessageEvent.Factory.TryCreateFrom(timelineEvent, roomId,
                             out TextMessageEvent textMessageEvent))
                    roomEvents.Add(textMessageEvent);

            return roomEvents;
        }

        public List<BaseRoomEvent> CreateFromInvited(string roomId, InvitedRoom invitedRoom)
        {
            var roomEvents = new List<BaseRoomEvent>();

            foreach (RoomStrippedState inviteStateEvent in invitedRoom.InviteState.Events)
                if (JoinRoomEvent.Factory.TryCreateFromStrippedState(inviteStateEvent, roomId,
                        out JoinRoomEvent joinRoomEvent))
                    roomEvents.Add(joinRoomEvent!);
                else if (CreateRoomEvent.Factory.TryCreateFromStrippedState(inviteStateEvent, roomId,
                             out CreateRoomEvent createRoomEvent))
                    roomEvents.Add(createRoomEvent!);
                else if (InviteToRoomEvent.Factory.TryCreateFromStrippedState(inviteStateEvent, roomId,
                             out InviteToRoomEvent inviteToRoomEvent))
                    roomEvents.Add(inviteToRoomEvent!);
                else if (TextMessageEvent.Factory.TryCreateFromStrippedState(inviteStateEvent, roomId,
                             out TextMessageEvent textMessageEvent))
                    roomEvents.Add(textMessageEvent);

            return roomEvents;
        }

        public List<BaseRoomEvent> CreateFromLeft(string roomId, LeftRoom leftRoom)
        {
            var roomEvents = new List<BaseRoomEvent>();

            foreach (Infrastructure.Dto.Sync.Event.Room.RoomEvent timelineEvent in leftRoom.Timeline.Events)
                if (JoinRoomEvent.Factory.TryCreateFrom(timelineEvent, roomId, out JoinRoomEvent joinRoomEvent))
                    roomEvents.Add(joinRoomEvent!);
                else if (CreateRoomEvent.Factory.TryCreateFrom(timelineEvent, roomId,
                             out CreateRoomEvent createRoomEvent))
                    roomEvents.Add(createRoomEvent!);
                else if (InviteToRoomEvent.Factory.TryCreateFrom(timelineEvent, roomId,
                             out InviteToRoomEvent inviteToRoomEvent))
                    roomEvents.Add(inviteToRoomEvent!);
                else if (TextMessageEvent.Factory.TryCreateFrom(timelineEvent, roomId,
                             out TextMessageEvent textMessageEvent))
                    roomEvents.Add(textMessageEvent);

            return roomEvents;
        }
    }
}