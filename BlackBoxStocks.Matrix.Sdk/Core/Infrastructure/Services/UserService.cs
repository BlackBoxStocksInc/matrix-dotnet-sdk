// ReSharper disable ArgumentsStyleNamedExpression

using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Login;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.User;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Extensions;

namespace BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Services
{
    public class UserService : BaseApiService
    {
        private ConcurrentDictionary<string, UserResponse> _users = new ConcurrentDictionary<string, UserResponse>();
        public UserService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<LoginResponse> LoginAsync(string user, string password, string deviceId,
            CancellationToken cancellationToken)
        {
            var model = new LoginRequest
            (
                new Identifier
                (
                    "m.id.user",
                    user
                ),
                password,
                deviceId,
                "m.login.password"
            );

            var httpClient = CreateHttpClient();

            var path = $"{ResourcePath}/login";

            return await httpClient.PostAsJsonAsync<LoginResponse>(path, model, cancellationToken);
        }

        public async Task<UserResponse> GetUser(string userId, CancellationToken cancellationToken)
        {
            if (_users.ContainsKey(userId))
            {
                return _users[userId];
            }

            var path = $"{BaseAddress}_synapse/admin/v2/users/{userId}";
            var httpClient = CreateHttpClient();

            UserResponse userResponse;
            try
            {
                userResponse = await httpClient.GetAsJsonAsync<UserResponse>(path, cancellationToken);
            }
            catch
            {
                userResponse = new UserResponse { DisplayName = userId };
            }

            _users.TryAdd(userId, userResponse);

            return userResponse;
        }
    }
}