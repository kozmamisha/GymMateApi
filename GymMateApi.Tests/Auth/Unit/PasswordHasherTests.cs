using GymMateApi.Infrastructure.Auth;
using Xunit;

namespace GymMateApi.Tests.Auth.Unit;

public class PasswordHasherTests
{
    private static PasswordHasher CreateSut() => new();

    [Fact]
    public void Generate_ReturnsNonEmptyHash()
    {
        var hash = CreateSut().Generate("P@ssw0rd!");

        Assert.False(string.IsNullOrWhiteSpace(hash));
    }

    [Fact]
    public void Generate_SamePasswordTwice_ProducesDifferentHashes()
    {
        // BCrypt uses a random salt per call
        var sut   = CreateSut();
        var hash1 = sut.Generate("same-password");
        var hash2 = sut.Generate("same-password");

        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void Generate_HashIsNotPlaintext()
    {
        const string password = "MyPassword123";
        var hash = CreateSut().Generate(password);

        Assert.DoesNotContain(password, hash);
    }

    [Fact]
    public void Generate_OutputLooksLikeBcryptHash()
    {
        var hash = CreateSut().Generate("anyPassword");

        // Enhanced BCrypt hashes start with $2a$, $2b$, or $2y$
        Assert.Matches(@"^\$2[aby]\$", hash);
    }

    [Fact]
    public void Verify_CorrectPassword_ReturnsTrue()
    {
        var sut  = CreateSut();
        var hash = sut.Generate("P@ssw0rd!");

        Assert.True(sut.Verify("P@ssw0rd!", hash));
    }

    [Fact]
    public void Verify_WrongPassword_ReturnsFalse()
    {
        var sut  = CreateSut();
        var hash = sut.Generate("correct-password");

        Assert.False(sut.Verify("wrong-password", hash));
    }

    [Fact]
    public void Verify_EmptyPassword_ReturnsFalse()
    {
        var sut  = CreateSut();
        var hash = sut.Generate("real-password");

        Assert.False(sut.Verify("", hash));
    }

    [Fact]
    public void Verify_PasswordWithDifferentCasing_ReturnsFalse()
    {
        var sut  = CreateSut();
        var hash = sut.Generate("CaseSensitive");

        Assert.False(sut.Verify("casesensitive", hash));
        Assert.False(sut.Verify("CASESENSITIVE", hash));
    }

    [Fact]
    public void Verify_TwoHashesOfSamePassword_BothVerifyCorrectly()
    {
        // Verifies that salt is embedded in the hash (standard BCrypt behaviour)
        var sut      = CreateSut();
        const string pass  = "same-password";
        var hash1    = sut.Generate(pass);
        var hash2    = sut.Generate(pass);

        Assert.True(sut.Verify(pass, hash1));
        Assert.True(sut.Verify(pass, hash2));
    }

    [Theory]
    [InlineData("short")]
    [InlineData("a very long password that exceeds typical limits but should still work fine with BCrypt")]
    [InlineData("P@$$w0rd!#€™∞")]   // special / unicode chars
    public void Verify_RoundTrip_VariousPasswords_ReturnsTrue(string password)
    {
        var sut  = CreateSut();
        var hash = sut.Generate(password);

        Assert.True(sut.Verify(password, hash));
    }
}