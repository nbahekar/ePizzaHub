namespace ePizzaHub.UI.Models.ApiModel.Response
{
    public class ValidateUserResponse
    {
        public string AccessToken { get; set; }

        public int TokenExpiresIn { get; set; }
    }
}
