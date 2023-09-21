using System.Collections.Generic;
using System.Linq;
using BlackBoxStocks.Matrix.Sdk.Core.Domain.MatrixRoom;
using BlackBoxStocks.Matrix.Sdk.Core.Domain.RoomEvent;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Sync;

namespace BlackBoxStocks.Matrix.Sdk.Core.Domain
{
    public record SyncBatch
    {
        private SyncBatch(string nextBatch, List<MatrixRoom.MatrixRoom> matrixRooms,
            List<BaseRoomEvent> matrixRoomEvents)
        {
            NextBatch = nextBatch;
            MatrixRooms = matrixRooms;
            MatrixRoomEvents = matrixRoomEvents;
        }

        public string NextBatch { get; }
        public List<MatrixRoom.MatrixRoom> MatrixRooms { get; }
        public List<BaseRoomEvent> MatrixRoomEvents { get; }

        internal static class Factory
        {
            private static readonly MatrixRoomFactory MatrixRoomFactory = new();
            private static readonly MatrixRoomEventFactory MatrixRoomEventFactory = new();

            public static SyncBatch CreateFromSync(string nextBatch, Rooms rooms)
            {
                List<MatrixRoom.MatrixRoom> matrixRooms = GetMatrixRoomsFromSync(rooms);
                List<BaseRoomEvent> matrixRoomEvents = GetMatrixEventsFromSync(rooms);

                return new SyncBatch(nextBatch, matrixRooms, matrixRoomEvents);
            }

            private static List<MatrixRoom.MatrixRoom> GetMatrixRoomsFromSync(Rooms rooms)
            {
                if (rooms == null) return new List<MatrixRoom.MatrixRoom>();
                
                var joinedMatrixRooms = rooms.Join != null ? rooms.Join.Select(pair => MatrixRoomFactory.CreateJoined(pair.Key, pair.Value))
                    .ToList(): new List<MatrixRoom.MatrixRoom>();
                var invitedMatrixRooms = rooms.Invite != null ? rooms.Invite
                    .Select(pair => MatrixRoomFactory.CreateInvite(pair.Key, pair.Value)).ToList() : new List<MatrixRoom.MatrixRoom>();
                var leftMatrixRooms = rooms.Leave != null ? rooms.Leave.Select(pair => MatrixRoomFactory.CreateLeft(pair.Key, pair.Value))
                    .ToList() : new List<MatrixRoom.MatrixRoom>();

                return joinedMatrixRooms.Concat(invitedMatrixRooms).Concat(leftMatrixRooms).ToList();
            }

            private static List<BaseRoomEvent> GetMatrixEventsFromSync(Rooms rooms)
            {
                if (rooms == null) return new List<BaseRoomEvent>();

                var joinedMatrixRoomEvents = rooms.Join != null ? rooms.Join
                    .SelectMany(pair => MatrixRoomEventFactory.CreateFromJoined(pair.Key, pair.Value)).ToList() : new List<BaseRoomEvent>();
                var invitedMatrixRoomEvents = rooms.Invite != null ? rooms.Invite
                    .SelectMany(pair => MatrixRoomEventFactory.CreateFromInvited(pair.Key, pair.Value)).ToList() : new List<BaseRoomEvent>();
                var leftMatrixRoomEvents = rooms.Leave != null ? rooms.Leave
                    .SelectMany(pair => MatrixRoomEventFactory.CreateFromLeft(pair.Key, pair.Value)).ToList() : new List<BaseRoomEvent>();

                return joinedMatrixRoomEvents.Concat(invitedMatrixRoomEvents).Concat(leftMatrixRoomEvents).ToList();
            }
        }
    }
}