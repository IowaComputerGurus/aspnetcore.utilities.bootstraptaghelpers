using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Form;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.FromFramework;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit.Abstractions;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.Form;

[UsesVerify]
public sealed class FormInputTagHelperTests : ModelTagHelperTest<FormInputTagHelper, TestModel>
{
    public FormInputTagHelperTests(ITestOutputHelper output) : base(output)
    {

    }

    [Fact]
    public async Task Renders()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: "Test", propertyName: nameof(TestModel.TextField));
        var output = await tagHelper.Render();
        await VerifyTagHelper(output);

    }
    internal override FormInputTagHelper TagHelperFactory(IHtmlGenerator htmlGenerator, ModelExpression modelExpression, ViewContext viewContext)
        => new(htmlGenerator) { For = modelExpression, ViewContext = viewContext };
}

