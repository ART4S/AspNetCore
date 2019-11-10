namespace AuthenticationExample.Security.Interfaces
{
    public interface ICookieService
    {
        void SignIn(int userId);

        void SignOut();
    }
}
