using Blazored.SessionStorage;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;


//https://learn.microsoft.com/en-us/aspnet/core/blazor/security/server/?view=aspnetcore-7.0&tabs=visual-studio 



namespace WebBlazor
{
    //public interface ICustomAuthStateProvider
    //{
    //    Task<AuthenticationState> GetAuthenticationStateAsync();
    //    void Authentication(string token);

    //}
    //public class CustomAuthStateProvider : AuthenticationStateProvider, ICustomAuthStateProvider
    //{
    //    private readonly HttpClient _http;
    //    private readonly ISessionStorageService _sessionStorageService;

    //    public CustomAuthStateProvider(HttpClient http, ISessionStorageService sessionStorageService)
    //    {
    //        _http = http;
    //        _sessionStorageService = sessionStorageService;
    //    }

    //    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    //    {
    //         string token = "";
    //        //var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiSmFudXN6IEtvd2Fsa293c2tpIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVXNlciIsImV4cCI6MTY3NjAxODEyMSwiaXNzIjoiaHR0cDovL3N3cHpwLmNvbSIsImF1ZCI6Imh0dHA6Ly9zd3B6cC5jb20ifQ.kvS9Z8F60Fw3e-BIQMaBC9lTTIwjaQYMPrkawq_MhSc";
    //        //var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjUiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4gQWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNYW5hZ2VyIiwiZXhwIjoxNjc2MDE4MDUzLCJpc3MiOiJodHRwOi8vc3dwenAuY29tIiwiYXVkIjoiaHR0cDovL3N3cHpwLmNvbSJ9.qq3ahmfTcXUAK_Q8Clcr6Fd92YJRK4DS3gjMl2SfyMU";
    //        //InvalidOperationException: JavaScript interop calls cannot be issued at this time. This is because the component is being statically rendered. When prerendering is enabled, JavaScript interop calls can only be performed during the OnAfterRenderAsync lifecycle method.
    //        //string token = "";


    //        // var token = await _sessionStorageService.GetItemAsStringAsync("token");


    //        var identity = new ClaimsIdentity();
    //        _http.DefaultRequestHeaders.Authorization = null;

    //        if (!string.IsNullOrEmpty(token))
    //        {
    //            identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
    //            _http.DefaultRequestHeaders.Authorization =
    //                new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
    //        }

    //        var user = new ClaimsPrincipal(identity);
    //        var state = new AuthenticationState(user);

    //        NotifyAuthenticationStateChanged(Task.FromResult(state));
    //        return state;
    //    }

    //    public async void Authentication(string token)
    //    {
    //        var identity = new ClaimsIdentity();
    //        _http.DefaultRequestHeaders.Authorization = null;

    //        if (!string.IsNullOrEmpty(token))
    //        {
    //            identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
    //            _http.DefaultRequestHeaders.Authorization =
    //                new AuthenticationHeaderValue("Bearer", token.Replace("\"", ""));
    //        }

    //        var user = new ClaimsPrincipal(identity);
    //        var state = new AuthenticationState(user);

    //        NotifyAuthenticationStateChanged(Task.FromResult(state));

    //        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    //    }


    //    public IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    //    {
    //        var payload = jwt.Split('.')[1];
    //        var jsonBytes = ParseBase64WithoutPadding(payload);
    //        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
    //        return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
    //    }

    //    private static byte[] ParseBase64WithoutPadding(string base64)
    //    {
    //        switch (base64.Length % 4)
    //        {
    //            case 2: base64 += "=="; break;
    //            case 3: base64 += "="; break;
    //        }
    //        return Convert.FromBase64String(base64);
    //    }

    //}
}
