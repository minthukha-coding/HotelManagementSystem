using NUlid;
using System.IdentityModel.Tokens.Jwt;

namespace HotelManagementSystem.Shared;

public static class _DevCode
{
    public static string GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        if (tokenHandler.CanReadToken(token))
        {
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Sub);

            if (userIdClaim != null)
            {
                return userIdClaim.Value;
            }
        }

        return null;
    }

    public static string GetUlid()
    {
        return Ulid.NewUlid().ToString();
    }

}