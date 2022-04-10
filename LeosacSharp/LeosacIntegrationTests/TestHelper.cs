using System;

namespace LeosacIntegrationTests;

public class TestHelper
{
    public static string GetClassNameDockerName<TX>()
    {
        var fullName = typeof(TX).FullName;
        if (fullName == null)
            throw new ApplicationException("Error getting test name");

        fullName = fullName.Replace("LeosacIntegrationTests.", "");

        if (fullName.Length > 63)
        {
            throw new ApplicationException("Test name too long");
        }

        return fullName.Replace(".", "_");
    }
}