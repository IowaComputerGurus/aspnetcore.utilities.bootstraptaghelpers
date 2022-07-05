using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Options;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.FromFramework;

public class TestOptionsManager : IOptions<AntiforgeryOptions>
{
    public TestOptionsManager()
    {
    }

    public TestOptionsManager(AntiforgeryOptions options)
    {
        Value = options;
    }

    public AntiforgeryOptions Value { get; set; } = new AntiforgeryOptions();
}