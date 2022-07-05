using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Form;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.FromFramework;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit.Abstractions;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.Form;

[UsesVerify]
public sealed class FormTextAreaTagHelperTests : ModelTagHelperTest<FormTextAreaTagHelper, TestModel>
{
    public FormTextAreaTagHelperTests(ITestOutputHelper output) : base(output)
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

    [Fact]
    public async Task Renders_Child_Content()
    {
        var metadataProvider = new TestModelMetadataProvider();
        var htmlGenerator = new TestableHtmlGenerator(metadataProvider);

        var tagHelper = GetTagHelper(htmlGenerator, model: "Test", propertyName: nameof(TestModel.TextField));
        var output = await tagHelper.Render(childContent: new HtmlString("Some child content"));

        await VerifyTagHelper(output);
    }

    internal override FormTextAreaTagHelper TagHelperFactory(IHtmlGenerator htmlGenerator, ModelExpression modelExpression, ViewContext viewContext)
        => new (htmlGenerator) { For = modelExpression, ViewContext = viewContext };
}

