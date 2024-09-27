namespace Muma.Api.JWT
{
    public interface IJWTHelper
    {
        string GetJWT(TokenGenerationRequest request);

        string GetRefreshJWT(TokenGenerationRequest request);

        (bool isValid, string user) ValidateRefreshJWT(string token);
    }
}
