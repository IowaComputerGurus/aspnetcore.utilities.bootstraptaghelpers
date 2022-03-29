using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Modal;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.Modal;

public class ModalHeaderDismissTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public void Should_Render_As_Button()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderDismissTagHelper();
        helper.Process(context, output);

        //Assert
        Assert.Equal("button", output.TagName);
    }

    [Theory]
    [InlineData("aria-label", "Close")]
    [InlineData("data-dismiss", "modal")]
    [InlineData("class", "close")]
    [InlineData("type", "button")]
    public void Should_Set_Needed_Attributes(string expectedAttribute, string expectedValue)
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderDismissTagHelper();
        helper.Process(context, output);

        //Assert
        Assert.Equal(expectedValue, output.Attributes[expectedAttribute].Value);
    }
}