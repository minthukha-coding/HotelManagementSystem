using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementSystem.Shared.Services.JwtService;

public class JwtAuthStateProviderService : AuthenticationStateProvider
{
    private readonly JwtTokenService _jwtTokenService;
    private readonly LocalStorageService _localStorageService;

    public JwtAuthStateProviderService(JwtTokenService jwtTokenService, LocalStorageService localStorageService)
    {
        _jwtTokenService = jwtTokenService;
        _localStorageService = localStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await GetTokenFromStorageAsync();

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var principal = _jwtTokenService.ReadJwtToken(token);

        if (principal == null)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        return new AuthenticationState(principal);
    }

    private async Task<string> GetTokenFromStorageAsync()
    {
        return await _localStorageService.GetItemAsync("authToken");
    }

    public void NotifyUserAuthentication(string token)
    {
        var principal = _jwtTokenService.ReadJwtToken(token);
        if(principal == null)
        {
            throw new ArgumentNullException(nameof(principal));
        }
        var authState = new AuthenticationState(principal);
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }

    public void NotifyUserLogout()
    {
        var authState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        NotifyAuthenticationStateChanged(Task.FromResult(authState));
    }
}