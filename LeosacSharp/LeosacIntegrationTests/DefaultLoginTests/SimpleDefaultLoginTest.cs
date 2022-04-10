using System.Threading.Tasks;
using LeosacAPI;
using Xunit;

namespace LeosacIntegrationTests.DefaultLoginTests;

public class CanLoginWithDefaultCredential : DefaultLoginTest<CanLoginWithDefaultCredential>
{
    [Fact]
    public async Task Test()
    {
        Assert.Equal(0, _api.UserId);
        await _api.AuthenticateAsync("admin", "admin", _cancellationToken);
        Assert.Equal(1, _api.UserId);
    }
}

public class CannotLoginWithInvalidCredential : DefaultLoginTest<CannotLoginWithInvalidCredential>
{
    [Fact]
    public async Task Test()
    {
        await Assert.ThrowsAsync<InvalidApiCredentialException>(() =>
            _api.AuthenticateAsync("admin", "wrong_password", _cancellationToken));
        Assert.Equal(0, _api.UserId);
    }
}
