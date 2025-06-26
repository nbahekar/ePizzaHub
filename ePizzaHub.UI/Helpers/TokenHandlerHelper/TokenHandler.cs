using System.Net.Http.Headers;

namespace ePizzaHub.UI.Helpers.TokenHandlerHelper
{
    public class TokenHandler(ITokenService _tokenService) : DelegatingHandler
    {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _tokenService.GetToken();

            if (token is not null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return base.SendAsync(request, cancellationToken);
        }

    }
}
