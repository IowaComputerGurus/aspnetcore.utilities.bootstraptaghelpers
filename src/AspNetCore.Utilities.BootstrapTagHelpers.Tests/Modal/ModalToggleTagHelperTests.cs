using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Modal;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.Modal;

public class ModalToggleTagHelperTests : AbstractTagHelperTest
{
    [Theory]
    [InlineData("", "button")]
    [InlineData("a", "a")]
    public void Should_Render_Proper_TagName(string setTagName, string expectedTagName)
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput("");

        //Act
        var helper = new ModalToggleTagHelper();
        if (!string.IsNullOrEmpty(setTagName))
            helper.TagName = setTagName;
        helper.Process(context, output);

        //Assert
        Assert.Equal(expectedTagName, output.TagName);
    }

    [Theory]
    [InlineData(null, "btn btn-primary")]
    [InlineData(BootstrapColor.Primary, "btn btn-primary")]
    [InlineData(BootstrapColor.Secondary, "btn btn-secondary")]
    public void Should_Render_Proper_CssClass(BootstrapColor? setColor, string expectedClass)
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput("");

        //Act
        var helper = new ModalToggleTagHelper();
        if (setColor != null)
            helper.ToggleColor = setColor.GetValueOrDefault();
        helper.Process(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public void Should_Render_DataToggle_Attribute()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalToggleTagHelper();
        helper.Process(context, output);

        //Assert
        Assert.Equal("modal", output.Attributes["data-toggle"].Value);
    }

    [Fact]
    public void Should_Render_DataTarget_Attribute()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalToggleTagHelper {Target = "testTarget"};
        helper.Process(context, output);

        //Assert
        Assert.Equal("#testTarget", output.Attributes["data-target"].Value);
    }

    [Fact]
    public void Should_Render_Type_Attribute_When_TagName_Button()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalToggleTagHelper();
        helper.Process(context, output);

        //Assert
        Assert.Equal("button", output.Attributes["type"].Value);
    }

    [Fact]
    public void ShouldNot_Render_Type_Attribute_When_TagName_IsNotButton()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalToggleTagHelper {TagName = "a"};
        helper.Process(context, output);

        //Assert
        Assert.Null(output.Attributes["type"]);
    }
}