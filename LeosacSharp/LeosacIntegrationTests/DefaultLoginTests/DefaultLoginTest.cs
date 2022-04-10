namespace LeosacIntegrationTests.DefaultLoginTests;

public class DefaultLoginTest<T> : LeosacTestBase
{
    protected DefaultLoginTest() : base("test_login_with_default_credentials",
        TestHelper.GetClassNameDockerName<T>())
    {
    }
}
