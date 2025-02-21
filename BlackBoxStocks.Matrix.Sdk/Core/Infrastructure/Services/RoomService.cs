﻿using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Room.Create;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Room.Join;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Room.Joined;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Extensions;

namespace BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Services
{
    public class RoomService : BaseApiService
    {
        public RoomService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<CreateRoomResponse> CreateRoomAsync(string accessToken, string[]? members,
            CancellationToken cancellationToken)
        {
            var model = new CreateRoomRequest
            (
                Invite: members,
                Preset: Preset.TrustedPrivateChat,
                IsDirect: true
            );

            HttpClient httpClient = CreateHttpClient(accessToken);

            var path = $"{ResourcePath}/createRoom";

            return await httpClient.PostAsJsonAsync<CreateRoomResponse>(path, model, cancellationToken);
        }

        public async Task<JoinRoomResponse> JoinRoomAsync(string accessToken, string roomId,
            CancellationToken cancellationToken)
        {
            HttpClient httpClient = CreateHttpClient(accessToken);

            var path = $"{ResourcePath}/rooms/{roomId}/join";

            return await httpClient.PostAsJsonAsync<JoinRoomResponse>(path, null, cancellationToken);
        }


        public async Task<JoinedRoomsResponse> GetJoinedRoomsAsync(string accessToken,
            CancellationToken cancellationToken)
        {
            HttpClient httpClient = CreateHttpClient(accessToken);

            var path = $"{ResourcePath}/joined_rooms";

            return await httpClient.GetAsJsonAsync<JoinedRoomsResponse>(path, cancellationToken);
        }

        public async Task LeaveRoomAsync(string accessToken, string roomId,
            CancellationToken cancellationToken)
        {
            HttpClient httpClient = CreateHttpClient(accessToken);

            var path = $"{ResourcePath}/rooms/{roomId}/leave";

            await httpClient.PostAsync(path, cancellationToken);
        }
    }
}