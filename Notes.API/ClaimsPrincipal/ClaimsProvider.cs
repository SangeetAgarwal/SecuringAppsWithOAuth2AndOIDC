namespace Notes.Api.ClaimsPrincipal;

public class ClaimsProvider
{
    public ClaimsProvider(System.Security.Claims.ClaimsPrincipal? claimsPrincipal)
    {
        ClaimsPrincipal = claimsPrincipal;
    }

    public System.Security.Claims.ClaimsPrincipal? ClaimsPrincipal { get; set; }
}