using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests;

public class BadgeTagHelperTests : AbstractTagHelperTest
{
    [Theory]
    [InlineData(BootstrapColor.Primary, "badge badge-primary")]
    [InlineData(BootstrapColor.Secondary, "badge badge-secondary")]
    [InlineData(BootstrapColor.Success, "badge badge-success")]
    [InlineData(BootstrapColor.Warning, "badge badge-warning")]
    [InlineData(BootstrapColor.Info, "badge badge-info")]
    [InlineData(BootstrapColor.Danger, "badge badge-danger")]
    public void Should_Render_ProperClass(BootstrapColor color, string expectedClass)
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput("");

        //Act
        var helper = new BadgeTagHelper {BadgeColor = color};
        helper.Process(context, output);

        //Assert
        Assert.Equal("span", output.TagName);
        Assert.Equal(expectedClass, output.Attributes["class"].Value);
    }

    [Fact]
    public void Should_NotRender_If_Display_Is_Hidden()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput("");

        //Act
        var helper = new BadgeTagHelper {HideDisplay = true};
        helper.Process(context, output);

        //Assert
        Assert.True(output.Content.IsEmptyOrWhiteSpace);
    }
}