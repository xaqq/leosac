using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeosacAPI;
using Xunit;

namespace LeosacIntegrationTests.DefaultLoginTests;

public class ChangePasswordFailureTest : DefaultLoginTest<ChangePasswordFailureTest>
{
    [Fact]
    public async Task Test()
    {
        await _api.AuthenticateAsync("admin", "admin", _cancellationToken);
        var e = await Assert.ThrowsAsync<LeosacApiException>(() =>
            _api.ChangePasswordAsync("wrong_old_password", "toto", _cancellationToken));
        Assert.Equal(APIStatusCode.PERMISSION_DENIED, e.StatusCode);
    }
}

public class ChangePasswordSuccessTest : DefaultLoginTest<ChangePasswordSuccessTest>
{
    [Fact]
    public async Task Test()
    {
        await _api.AuthenticateAsync("admin", "admin", _cancellationToken);
        await _api.ChangePasswordAsync("admin", "toto", _cancellationToken);

        var newWsApi = await CreateAndWaitLeosacApi();

        // Old password don't work anymore
        await Assert.ThrowsAsync<InvalidApiCredentialException>(() =>
            newWsApi.AuthenticateAsync("admin", "admin", _cancellationToken));

        // New password ok
        await newWsApi.AuthenticateAsync("admin", "toto", _cancellationToken);
    }
}

public class ChangePasswordCloseOtherSessionTest : DefaultLoginTest<ChangePasswordCloseOtherSessionTest>
{
    [Fact]
    public async Task Test()
    {
        var connectionA = await CreateAndWaitLeosacApi();
        var connectionB = await CreateAndWaitLeosacApi();
        var connectionC = await CreateAndWaitLeosacApi();
        var connections = new List<LeosacApi> { connectionA, connectionB, connectionC };

        await Task.WhenAll(connections.Select(x =>
        {
            var task = x.AuthenticateAsync("admin", "admin", _cancellationToken);
            return task;
        }).ToArray());

        await connectionA.ChangePasswordAsync("admin", "toto", _cancellationToken);

        // Other connections are logged out and can't change password anymore.

        // connectionB ...
        var e = await Assert.ThrowsAsync<LeosacApiException>(() =>
            connectionB.ChangePasswordAsync("toto", "titi", _cancellationToken));
        Assert.Equal(APIStatusCode.PERMISSION_DENIED, e.StatusCode);
        
        // ... connectionC
        e = await Assert.ThrowsAsync<LeosacApiException>(() =>
            connectionC.ChangePasswordAsync("toto", "titi", _cancellationToken));
        Assert.Equal(APIStatusCode.PERMISSION_DENIED, e.StatusCode);

        // However connectionA is still properly authenticated.
        await connectionA.ChangePasswordAsync("toto", "super_final_password", _cancellationToken);
    }
}