using System.Threading.Tasks;
using LeosacAPI;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LeosacIntegrationTests.DefaultLoginTests;


public class TestDatabaseError : DefaultLoginTest<TestDatabaseError>
{
    [Fact]
    public async Task Test()
    {
        // Drop the database container
        await helper.DropPostgres(_cancellationToken);
        LeosacApiException exception = await Assert.ThrowsAsync<LeosacApiException>(() =>
            _api.AuthenticateAsync("admin", "admin", _cancellationToken));

        Assert.Equal(APIStatusCode.DATABASE_ERROR, exception.StatusCode);
    }
}

public class LoginWithValidCredentialLeaveAudit : DefaultLoginTest<LoginWithValidCredentialLeaveAudit>
{
    [Fact]
    public async Task Test()
    {
        await _api.AuthenticateAsync("admin", "admin", _cancellationToken);
        using (var db = await GetDbContext())
        {
            var audit = await db.AuditEntries.Include(x => x.Wsapicall).FirstAsync();
            Assert.Equal("create_auth_token", audit.Wsapicall.ApiMethod);

            // Status means processing went ok, not necessarily that login went ok
            Assert.Equal((int)APIStatusCode.SUCCESS, audit.Wsapicall.StatusCode);
        }
    }
}

public class LoginWithInvalidCredentialLeavesAudit : DefaultLoginTest<LoginWithInvalidCredentialLeavesAudit>
{
    [Fact]
    public async Task Test()
    {
        await Assert.ThrowsAsync<InvalidApiCredentialException>(() => _api.AuthenticateAsync("admin", "wrong_password", _cancellationToken));
        using (var db = await GetDbContext())
        {
            var audit = await db.AuditEntries.Include(x => x.Wsapicall).FirstAsync();
            Assert.Equal("create_auth_token", audit.Wsapicall.ApiMethod);

            // Status means processing went ok, not necessarily that login went ok
            Assert.Equal((int)APIStatusCode.SUCCESS, audit.Wsapicall.StatusCode);
        }
    }
}
