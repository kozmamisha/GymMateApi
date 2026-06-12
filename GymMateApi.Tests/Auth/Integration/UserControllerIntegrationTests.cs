using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using DotNet.Testcontainers.Builders;
using GymMateApi.Contracts.User;
using GymMateApi.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;
using Xunit;

namespace GymMateApi.Tests.Auth.Integration;

public class UserControllerIntegrationTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:16-alpine")
        .WithDatabase("gymmate_test")
        .WithUsername("postgres")
        .WithPassword("test")
        .Build();

    private WebApplicationFactory<Program> _factory = null!;
    private HttpClient _client = null!;

    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();

        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Replace real DB with test-container DB
                var descriptor =
                    services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<GymMateDbContext>));
                if (descriptor is not null)
                    services.Remove(descriptor);

                services.AddDbContext<GymMateDbContext>(opts =>
                    opts.UseNpgsql(_postgres.GetConnectionString()));
            });

            builder.UseSetting("JwtOptions:SecretKey",
                "integration-test-secret-that-is-long-enough-32plus!");
            builder.UseSetting("JwtOptions:ExpiresHours", "1");
            builder.UseSetting("Auth:CookieName", "test-cookie");
        });

        // Apply EF migrations
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<GymMateDbContext>();
        await db.Database.MigrateAsync();

        _client = _factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        _client.Dispose();
        await _factory.DisposeAsync();
        await _postgres.DisposeAsync();
    }

    // ─── helpers ─────────────────────────────────────────────────────────────

    private static RegisterUserRequest MakeRegisterRequest(
        string userName = "TestUser",
        string email = "test@gym.com",
        string password = "P@ssw0rd!")
    {
        return new RegisterUserRequest { UserName = userName, Email = email, Password = password };
    }

    /// <summary>Registers and logs in, returns the JWT string from the response body.</summary>
    private async Task<string> RegisterAndLoginAsync(
        string email = "test@gym.com",
        string password = "P@ssw0rd!",
        string userName = "TestUser")
    {
        await _client.PostAsJsonAsync("/api/user/register",
            MakeRegisterRequest(userName, email, password));

        var loginResp = await _client.PostAsJsonAsync("/api/user/login",
            new LoginUserRequest { Email = email, Password = password });

        return (await loginResp.Content.ReadAsStringAsync()).Trim('"');
    }

    private static HttpClient WithBearer(HttpClient client, string token)
    {
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    // ════════════════════════════════════════════════════════════════════════
    // POST /api/user/register
    // ════════════════════════════════════════════════════════════════════════

    [Fact]
    public async Task Register_ValidRequest_Returns200Ok()
    {
        var response = await _client.PostAsJsonAsync("/api/user/register",
            MakeRegisterRequest(email: "register-ok@gym.com"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Register_DuplicateEmail_Returns400BadRequest()
    {
        await _client.PostAsJsonAsync("/api/user/register",
            MakeRegisterRequest(email: "dup@gym.com"));

        var response = await _client.PostAsJsonAsync("/api/user/register",
            MakeRegisterRequest(email: "dup@gym.com", userName: "OtherUser"));

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_DuplicateEmail_ResponseBodyContainsMessage()
    {
        await _client.PostAsJsonAsync("/api/user/register",
            MakeRegisterRequest(email: "dup2@gym.com"));

        var response = await _client.PostAsJsonAsync("/api/user/register",
            MakeRegisterRequest(email: "dup2@gym.com"));
        var body = await response.Content.ReadAsStringAsync();

        Assert.Contains("already exists", body, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("", "a@b.com", "pass")]
    [InlineData("usr", "", "pass")]
    [InlineData("usr", "a@b.com", "")]
    public async Task Register_MissingFields_Returns400BadRequest(
        string userName, string email, string password)
    {
        var response = await _client.PostAsJsonAsync("/api/user/register",
            new RegisterUserRequest { UserName = userName, Email = email, Password = password });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // ════════════════════════════════════════════════════════════════════════
    // POST /api/user/login
    // ════════════════════════════════════════════════════════════════════════

    [Fact]
    public async Task Login_ValidCredentials_Returns200Ok()
    {
        await _client.PostAsJsonAsync("/api/user/register",
            MakeRegisterRequest(email: "login-ok@gym.com"));

        var response = await _client.PostAsJsonAsync("/api/user/login",
            new LoginUserRequest { Email = "login-ok@gym.com", Password = "P@ssw0rd!" });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Login_ValidCredentials_ReturnsNonEmptyToken()
    {
        await _client.PostAsJsonAsync("/api/user/register",
            MakeRegisterRequest(email: "token@gym.com"));

        var response = await _client.PostAsJsonAsync("/api/user/login",
            new LoginUserRequest { Email = "token@gym.com", Password = "P@ssw0rd!" });

        var body = (await response.Content.ReadAsStringAsync()).Trim('"');
        Assert.False(string.IsNullOrWhiteSpace(body));
        // JWT has exactly 3 dot-separated segments
        Assert.Equal(3, body.Split('.').Length);
    }

    [Fact]
    public async Task Login_WrongPassword_Returns401Unauthorized()
    {
        await _client.PostAsJsonAsync("/api/user/register",
            MakeRegisterRequest(email: "wp@gym.com"));

        var response = await _client.PostAsJsonAsync("/api/user/login",
            new LoginUserRequest { Email = "wp@gym.com", Password = "WrongPass!" });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Login_UnknownEmail_Returns401Unauthorized()
    {
        var response = await _client.PostAsJsonAsync("/api/user/login",
            new LoginUserRequest { Email = "ghost@gym.com", Password = "anyPass" });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Theory]
    [InlineData("", "pass")]
    [InlineData("a@b.com", "")]
    public async Task Login_MissingFields_Returns400BadRequest(string email, string password)
    {
        var response = await _client.PostAsJsonAsync("/api/user/login",
            new LoginUserRequest { Email = email, Password = password });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // ════════════════════════════════════════════════════════════════════════
    // POST /api/user/logout
    // ════════════════════════════════════════════════════════════════════════

    [Fact]
    public async Task Logout_Authenticated_Returns204NoContent()
    {
        var token = await RegisterAndLoginAsync("logout@gym.com");
        var client = _factory.CreateClient();
        WithBearer(client, token);

        var response = await client.PostAsync("/api/user/logout", null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Logout_NotAuthenticated_Returns401Unauthorized()
    {
        var response = await _factory.CreateClient()
            .PostAsync("/api/user/logout", null);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    // ════════════════════════════════════════════════════════════════════════
    // DELETE /api/user/{id}
    // ════════════════════════════════════════════════════════════════════════

    [Fact]
    public async Task Delete_OwnAccount_Returns204NoContent()
    {
        const string email = "self-delete@gym.com";
        var token = await RegisterAndLoginAsync(email);

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<GymMateDbContext>();
        var user = await db.Users.FirstAsync(u => u.Email == email);

        var client = _factory.CreateClient();
        WithBearer(client, token);

        var response = await client.DeleteAsync($"/api/user/{user.Id}");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Delete_NotAuthenticated_Returns401Unauthorized()
    {
        var response = await _factory.CreateClient()
            .DeleteAsync($"/api/user/{Guid.NewGuid()}");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Delete_AnotherUsersAccount_Returns400BadRequest()
    {
        // Register user A and get their token
        var tokenA = await RegisterAndLoginAsync(
            "user-a-del@gym.com", userName: "UserA");

        // Register user B (just to get their ID)
        await _client.PostAsJsonAsync("/api/user/register",
            MakeRegisterRequest(email: "user-b-del@gym.com", userName: "UserB"));

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<GymMateDbContext>();
        var userB = await db.Users.FirstAsync(u => u.Email == "user-b-del@gym.com");

        // Try to delete user B while authenticated as user A
        var client = _factory.CreateClient();
        WithBearer(client, tokenA);

        var response = await client.DeleteAsync($"/api/user/{userB.Id}");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // ════════════════════════════════════════════════════════════════════════
    // Full auth flow
    // ════════════════════════════════════════════════════════════════════════

    [Fact]
    public async Task FullFlow_RegisterLoginProtectedEndpoint_Works()
    {
        // Register
        var registerResp = await _client.PostAsJsonAsync("/api/user/register",
            MakeRegisterRequest(email: "flow@gym.com"));
        Assert.Equal(HttpStatusCode.OK, registerResp.StatusCode);

        // Login — get token from body
        var loginResp = await _client.PostAsJsonAsync("/api/user/login",
            new LoginUserRequest { Email = "flow@gym.com", Password = "P@ssw0rd!" });
        Assert.Equal(HttpStatusCode.OK, loginResp.StatusCode);

        var token = (await loginResp.Content.ReadAsStringAsync()).Trim('"');
        Assert.False(string.IsNullOrWhiteSpace(token));

        // Use token to hit a protected endpoint
        var client = _factory.CreateClient();
        WithBearer(client, token);

        var logoutResp = await client.PostAsync("/api/user/logout", null);
        Assert.Equal(HttpStatusCode.NoContent, logoutResp.StatusCode);
    }
}