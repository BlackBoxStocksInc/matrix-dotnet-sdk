using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlackBoxStocks.Matrix.Sdk.Core.Domain.MatrixRoom;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Room.Create;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Room.Join;

namespace BlackBoxStocks.Matrix.Sdk
{
    /// <summary>
    ///     A Client for interaction with Matrix.
    /// </summary>
    public interface IMatrixClient
    {
        string UserId { get; }

        Uri? BaseAddress { get; }

        bool IsLoggedIn { get; }
        
        bool IsSyncing { get; }

        MatrixRoom[] InvitedRooms { get; }

        MatrixRoom[] JoinedRooms { get; }

        MatrixRoom[] LeftRooms { get; }
        
        event EventHandler<MatrixRoomEventsEventArgs> OnMatrixRoomEventsReceived;

        Task LoginAsync(Uri baseAddress, string user, string password, string deviceId);

        void LoginWithAccessTokenAsync(Uri baseAddress, string userId, string accessToken);

        void Start(string? nextBatch = null);

        void Stop();

        Task<CreateRoomResponse> CreateTrustedPrivateRoomAsync(string[] invitedUserIds);

        Task<JoinRoomResponse> JoinTrustedPrivateRoomAsync(string roomId);

        Task<string> SendMessageAsync(string roomId, string message);

        Task<List<string>> GetJoinedRoomsIdsAsync();

        Task LeaveRoomAsync(string roomId);
    }
}