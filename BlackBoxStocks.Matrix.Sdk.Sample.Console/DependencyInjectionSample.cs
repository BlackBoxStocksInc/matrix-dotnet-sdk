using BlackBoxStocks.Matrix.Sdk;
using BlackBoxStocks.Matrix.Sdk.Core.Domain.RoomEvent;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Room.Create;

namespace Matrix.Sdk.Sample.Console
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using Sodium;

    public class DependencyInjectionSample
    {
        
        private static readonly CryptographyService CryptographyService = new();

        private record LoginRequest(Uri BaseAddress, string Username, string Password, string DeviceId);

        private static LoginRequest CreateLoginRequest()
        {
            var seed = Guid.NewGuid().ToString();
            KeyPair keyPair = CryptographyService.GenerateEd25519KeyPair(seed);

            byte[] loginDigest = CryptographyService.GenerateLoginDigest();
            string hexSignature = CryptographyService.GenerateHexSignature(loginDigest, keyPair.PrivateKey);
            string publicKeyHex = CryptographyService.ToHexString(keyPair.PublicKey);
            string hexId = CryptographyService.GenerateHexId(keyPair.PublicKey);

            var password = $"ed:{hexSignature}:{publicKeyHex}";
            string deviceId = publicKeyHex;

            var baseAddress = new Uri("https://beacon-node-0.papers.tech:8448/");
            
            var loginRequest = new LoginRequest(baseAddress, hexId, password, deviceId);

            return loginRequest;
        }
        
        public async Task Run(IServiceProvider serviceProvider)
        {
            IMatrixClient client = serviceProvider.GetRequiredService<IMatrixClient>();
            IMatrixClient anotherClient = serviceProvider.GetRequiredService<IMatrixClient>();

            client.OnMatrixRoomEventsReceived += (sender, eventArgs) =>
            {
                foreach (BaseRoomEvent roomEvent in eventArgs.MatrixRoomEvents)
                {
                    if (roomEvent is not TextMessageEvent textMessageEvent)
                        continue;

                    (string roomId, string senderUserId, string message) = textMessageEvent;
                    if (client.UserId != senderUserId)
                        Console.WriteLine($"RoomId: {roomId} received message from {senderUserId}: {message}.");
                }
            };

            anotherClient.OnMatrixRoomEventsReceived += (sender, eventArgs) =>
            {
                foreach (BaseRoomEvent roomEvent in eventArgs.MatrixRoomEvents)
                {
                    if (roomEvent is not TextMessageEvent textMessageEvent)
                        continue;

                    (string roomId, string senderUserId, string message) = textMessageEvent;
                    if (anotherClient.UserId != senderUserId)
                        Console.WriteLine($"RoomId: {roomId} received message from {senderUserId}: {message}.");
                }
            };

            (Uri matrixNodeAddress, string username, string password, string deviceId) = CreateLoginRequest();
            await client.LoginAsync(matrixNodeAddress, username, password, deviceId);
            
            LoginRequest request2 = CreateLoginRequest();
            await anotherClient.LoginAsync(request2.BaseAddress, request2.Username, request2.Password, request2.DeviceId);

            client.Start();
            anotherClient.Start();
            
            CreateRoomResponse createRoomResponse = await client.CreateTrustedPrivateRoomAsync(new[]
            {
                anotherClient.UserId
            });
            
            await anotherClient.JoinTrustedPrivateRoomAsync(createRoomResponse.RoomId);
            
            var spin = new SpinWait();
            while (anotherClient.JoinedRooms.Length == 0)
                spin.SpinOnce();
            
            await client.SendMessageAsync(createRoomResponse.RoomId, "Hello");
            await anotherClient.SendMessageAsync(anotherClient.JoinedRooms[0].Id, ", ");
            
            await client.SendMessageAsync(createRoomResponse.RoomId, "World");
            await anotherClient.SendMessageAsync(anotherClient.JoinedRooms[0].Id, "!");

            Console.WriteLine($"client.IsLoggedIn: {client.IsLoggedIn}");
            Console.WriteLine($"client.IsSyncing: {client.IsSyncing}");
            
            // await client.GetJoinedRoomsIdsAsync();
            // string roomId = string.Empty;
            // await client.LeaveRoomAsync(roomId);
            
            Console.ReadLine();

            client.Stop();
            anotherClient.Stop();
            
            Console.WriteLine($"client.IsLoggedIn: {client.IsLoggedIn}");
            Console.WriteLine($"client.IsSyncing: {client.IsSyncing}");
        }
    }
}