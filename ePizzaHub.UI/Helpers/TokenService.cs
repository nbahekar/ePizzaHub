﻿namespace ePizzaHub.UI.Helpers
{
    public class TokenService : ITokenService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenService(IHttpContextAccessor httpContextAccessor) {

            this._httpContextAccessor = httpContextAccessor;
        }
        public string GetToken()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                return _httpContextAccessor.HttpContext.Request.Cookies["AccessToken"];
            }
            throw new Exception("Access Token Not exist");
        }

        public void SetToken(string token)
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                _httpContextAccessor.HttpContext.Response.Cookies.Append("AccessToken", token,
                    new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddHours(1)

                    });
            }
        }

    }
}
