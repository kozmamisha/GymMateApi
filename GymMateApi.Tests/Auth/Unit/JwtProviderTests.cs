using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GymMateApi.AuthService.Core;
using GymMateApi.AuthService.Infrastructure.Auth;
using GymMateApi.Shared.Auth;
using GymMateApi.Shared.Constants;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Xunit;

namespace GymMateApi.Tests.Auth.Unit;

public class JwtProviderTests
{
    private const string SecretKey    = "super-secret-key-for-tests-32-bytes!!!!!";
    private const int    ExpiresHours = 2;

    private static JwtProvider CreateSut(string? secret = null, int hours = ExpiresHours)
    {
        var opts = Options.Create(new JwtOptions
        {
            SecretKey    = secret ?? SecretKey,
            ExpiresHours = hours
        });
        return new JwtProvider(opts);
    }

    private static UserEntity MakeUser(string role = Roles.Admin) => new()
    {
        Id       = Guid.NewGuid(),
        UserName = "gymuser",
        Email    = "user@gym.com",
        Role     = role
    };

    private static JwtSecurityToken ParseToken(string tokenString)
    {
        var handler = new JwtSecurityTokenHandler();
        var key     = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        handler.ValidateToken(tokenString, new TokenValidationParameters
        {
            ValidateIssuer           = false,
            ValidateAudience         = false,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey         = key,
            ClockSkew                = TimeSpan.Zero
        }, out var validated);

        return (JwtSecurityToken)validated;
    }

    [Fact]
    public void GenerateToken_ReturnsNonEmptyString()
    {
        var token = CreateSut().GenerateToken(MakeUser());

        Assert.False(string.IsNullOrWhiteSpace(token));
    }

    [Fact]
    public void GenerateToken_ProducesValidJwtFormat()
    {
        var tokenString = CreateSut().GenerateToken(MakeUser());

        // A valid JWT has exactly 3 dot-separated segments
        var parts = tokenString.Split('.');
        Assert.Equal(3, parts.Length);
    }

    [Fact]
    public void GenerateToken_SignatureCanBeVerifiedWithSameKey()
    {
        var user        = MakeUser();
        var tokenString = CreateSut().GenerateToken(user);

        // ParseToken internally calls ValidateToken — throws if invalid
        var exception = Record.Exception(() => ParseToken(tokenString));
        Assert.Null(exception);
    }

    [Fact]
    public void GenerateToken_SignatureFailsWithDifferentKey()
    {
        var user        = MakeUser();
        var tokenString = CreateSut(secret: SecretKey).GenerateToken(user);

        var handler     = new JwtSecurityTokenHandler();
        var wrongKey    = new SymmetricSecurityKey(
            "completely-different-key-that-is-32-bytes-long!"u8.ToArray());

        Assert.Throws<SecurityTokenSignatureKeyNotFoundException>(() =>
            handler.ValidateToken(tokenString, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey         = wrongKey,
                ValidateIssuer           = false,
                ValidateAudience         = false
            }, out _));
    }

    [Fact]
    public void GenerateToken_ContainsUserIdClaim()
    {
        var user    = MakeUser();
        var parsed  = ParseToken(CreateSut().GenerateToken(user));
        var claim   = parsed.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);

        Assert.NotNull(claim);
        Assert.Equal(user.Id.ToString(), claim.Value);
    }

    [Fact]
    public void GenerateToken_ContainsUserNameClaim()
    {
        var user   = MakeUser();
        var parsed = ParseToken(CreateSut().GenerateToken(user));
        var claim  = parsed.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserName);

        Assert.NotNull(claim);
        Assert.Equal(user.UserName, claim.Value);
    }

    [Fact]
    public void GenerateToken_ContainsRoleClaim()
    {
        var user   = MakeUser(role: Roles.Admin);
        var parsed = ParseToken(CreateSut().GenerateToken(user));
        var claim  = parsed.Claims.FirstOrDefault(c => c.Type == CustomClaims.Role);

        Assert.NotNull(claim);
        Assert.Equal(Roles.Admin, claim.Value);
    }

    [Fact]
    public void GenerateToken_DifferentRoles_RoleClaimReflectsUserRole()
    {
        var adminUser  = MakeUser(role: "Admin");
        var regularUser = MakeUser(role: "User");

        var adminToken  = ParseToken(CreateSut().GenerateToken(adminUser));
        var regularToken = ParseToken(CreateSut().GenerateToken(regularUser));

        var adminRole   = adminToken.Claims.First(c => c.Type == CustomClaims.Role).Value;
        var regularRole = regularToken.Claims.First(c => c.Type == CustomClaims.Role).Value;

        Assert.Equal("Admin", adminRole);
        Assert.Equal("User", regularRole);
    }

    [Fact]
    public void GenerateToken_DifferentUsers_DifferentUserIdClaims()
    {
        var sut    = CreateSut();
        var user1  = MakeUser();
        var user2  = MakeUser();

        var token1 = ParseToken(sut.GenerateToken(user1));
        var token2 = ParseToken(sut.GenerateToken(user2));

        var id1 = token1.Claims.First(c => c.Type == CustomClaims.UserId).Value;
        var id2 = token2.Claims.First(c => c.Type == CustomClaims.UserId).Value;

        Assert.NotEqual(id1, id2);
    }

    [Fact]
    public void GenerateToken_ExpiresAfterConfiguredHours()
    {
        var before  = DateTime.UtcNow;
        var user    = MakeUser();
        var parsed  = ParseToken(CreateSut(hours: ExpiresHours).GenerateToken(user));
        var after   = DateTime.UtcNow;

        var expectedMin = before.AddHours(ExpiresHours).AddSeconds(-5);
        var expectedMax = after.AddHours(ExpiresHours).AddSeconds(5);

        Assert.InRange(parsed.ValidTo, expectedMin, expectedMax);
    }

    [Fact]
    public void GenerateToken_AlgorithmIsHmacSha256()
    {
        var tokenString = CreateSut().GenerateToken(MakeUser());
        var handler     = new JwtSecurityTokenHandler();
        var raw         = handler.ReadJwtToken(tokenString);

        Assert.Equal(SecurityAlgorithms.HmacSha256, raw.Header.Alg);
    }
}