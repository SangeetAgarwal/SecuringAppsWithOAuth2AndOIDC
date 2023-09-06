namespace Notes.MvcApp.Services.OidcServices
{
    public interface ITokenGenerator
    {
        public string GenerateSignedToken(string clientId, string audience);
    }
}
