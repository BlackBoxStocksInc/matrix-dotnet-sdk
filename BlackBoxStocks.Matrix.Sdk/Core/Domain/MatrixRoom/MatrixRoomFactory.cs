using System.Collections.Generic;
using BlackBoxStocks.Matrix.Sdk.Core.Domain.RoomEvent;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Sync;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Sync.Event.Room;

namespace BlackBoxStocks.Matrix.Sdk.Core.Domain.MatrixRoom
{
    public class MatrixRoomFactory
    {
        public MatrixRoom CreateJoined(string roomId, JoinedRoom joinedRoom)
        {
            var joinedUserIds = new List<string>();
            foreach (Infrastructure.Dto.Sync.Event.Room.RoomEvent timelineEvent in joinedRoom.Timeline.Events)
                if (JoinRoomEvent.Factory.TryCreateFrom(timelineEvent, roomId, out JoinRoomEvent joinRoomEvent))
                    joinedUserIds.Add(joinRoomEvent!.SenderUserId);

            return new MatrixRoom(roomId, MatrixRoomStatus.Joined, joinedUserIds);
        }

        public MatrixRoom CreateInvite(string roomId, InvitedRoom invitedRoom)
        {
            var joinedUserIds = new List<string>();
            foreach (RoomStrippedState timelineEvent in invitedRoom.InviteState.Events)
                if (JoinRoomEvent.Factory.TryCreateFromStrippedState(timelineEvent, roomId,
                        out JoinRoomEvent joinRoomEvent))
                    joinedUserIds.Add(joinRoomEvent!.SenderUserId);

            return new MatrixRoom(roomId, MatrixRoomStatus.Invited, joinedUserIds);
        }

        public MatrixRoom CreateLeft(string roomId, LeftRoom leftRoom)
        {
            var joinedUserIds = new List<string>();
            foreach (Infrastructure.Dto.Sync.Event.Room.RoomEvent timelineEvent in leftRoom.Timeline.Events)
                if (JoinRoomEvent.Factory.TryCreateFrom(timelineEvent, roomId, out JoinRoomEvent joinRoomEvent))
                    joinedUserIds.Add(joinRoomEvent!.SenderUserId);

            return new MatrixRoom(roomId, MatrixRoomStatus.Left, joinedUserIds);
        }
    }
}