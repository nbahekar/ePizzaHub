namespace ePizzaHub.UI.Helpers
{
    public interface ITokenService
    {

        void SetToken(string token);

        string GetToken();
    }
}
