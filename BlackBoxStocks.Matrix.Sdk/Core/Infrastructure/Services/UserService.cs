// ReSharper disable ArgumentsStyleNamedExpression

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Dto.Login;
using BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Extensions;

namespace BlackBoxStocks.Matrix.Sdk.Core.Infrastructure.Services
{
    public class UserService : BaseApiService
    {
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

            HttpClient httpClient = CreateHttpClient();

            var path = $"{ResourcePath}/login";

            return await httpClient.PostAsJsonAsync<LoginResponse>(path, model, cancellationToken);
        }
    }
}