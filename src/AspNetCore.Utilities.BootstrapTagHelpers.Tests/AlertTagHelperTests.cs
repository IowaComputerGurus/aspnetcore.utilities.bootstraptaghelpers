using System.Threading.Tasks;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests;

public class AlertTagHelperTests : AbstractTagHelperTest
{

    [Fact]
    public void Should_NotRender_If_Display_Is_Hidden()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput("");

        //Act
        var helper = new AlertTagHelper() { HideDisplay = true };
        helper.Process(context, output);

        //Assert
        Assert.True(output.Content.IsEmptyOrWhiteSpace);
    }

    [Theory]
    [InlineData(BootstrapColor.Primary, "alert alert-primary")]
    [InlineData(BootstrapColor.Secondary, "alert alert-secondary")]
    [InlineData(BootstrapColor.Success, "alert alert-success")]
    [InlineData(BootstrapColor.Warning, "alert alert-warning")]
    [InlineData(BootstrapColor.Info, "alert alert-info")]
    [InlineData(BootstrapColor.Danger, "alert alert-danger")]
    public async Task Should_Render_ProperClass(BootstrapColor color, string expectedClass)
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput("");

        //Act
        var helper = new AlertTagHelper {AlertColor = color};
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public async Task Should_Default_To_Information_Alert()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput("");
        var expectedClass = "alert alert-info";

        //Act
        var helper = new AlertTagHelper { };
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public async Task Should_Render_ProperClass_WhenDismissible()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput("");
        var expectedClass = "alert alert-info alert-dismissible fade show";

        //Act
        var helper = new AlertTagHelper { Dismissible = true};
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
        
    }

    [Fact]
    public async Task Should_Render_With_RoleAdded()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new AlertTagHelper {AlertColor = BootstrapColor.Info};
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
        Assert.Equal("alert", output.Attributes["role"].Value);
    }
}