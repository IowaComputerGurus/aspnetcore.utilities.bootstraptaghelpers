using System.Collections.Generic;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Environment;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.Environment;

public class EnvironmentAlertTagHelperTests : AbstractTagHelperTest
{
    [Theory]
    [InlineData(null, "<div class=\"alert alert-warning\">", "</div>")]
    [InlineData(BootstrapColor.Warning, "<div class=\"alert alert-warning\">", "</div>")]
    [InlineData(BootstrapColor.Danger, "<div class=\"alert alert-danger\">", "</div>")]
    public void Should_Render_WrappedContent_When_Not_Excluded(BootstrapColor? targetColor, string expectedPreContent,
        string expectedPostContent)
    {
        //Arrange
        var context = MakeTagHelperContext();
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("exclude", "Production")});
        var output = MakeTagHelperOutput(" ", existingAttributes);
        var mockEnvironment = new Mock<IWebHostEnvironment>();
        mockEnvironment.SetupProperty(g => g.EnvironmentName, "Development");

        //Act
        var helper = new EnvironmentAlertTagHelper(mockEnvironment.Object);
        if (targetColor.HasValue)
            helper.AlertColor = targetColor.Value;
        helper.Process(context, output);

        //Assert
        Assert.Equal(expectedPreContent, output.PreContent.GetContent());
        Assert.Equal(expectedPostContent, expectedPostContent);
    }

    [Fact]
    public void Should_Not_Wrap_Content_When_Excluded()
    {
        //Arrange
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("exclude", "Development")});
        var context = MakeTagHelperContext(existingAttributes);
        var output = MakeTagHelperOutput("environment", childContent: "My Alert Text");
        var mockEnvironment = new Mock<IWebHostEnvironment>();
        mockEnvironment.SetupProperty(g => g.EnvironmentName, "Development");

        //Act
        var helper = new EnvironmentAlertTagHelper(mockEnvironment.Object){Exclude = "Development"};
        helper.Process(context, output);

        //Assert
        Assert.Empty(output.PreContent.GetContent());
    }
}