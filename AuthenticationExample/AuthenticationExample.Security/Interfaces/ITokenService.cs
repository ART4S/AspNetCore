namespace AuthenticationExample.Security.Interfaces
{
    public interface ITokenService
    {
        string GetToken(int userId);
    }
}
