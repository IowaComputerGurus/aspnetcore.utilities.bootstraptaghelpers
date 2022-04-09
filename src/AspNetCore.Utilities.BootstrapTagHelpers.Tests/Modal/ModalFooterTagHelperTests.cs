using System.Collections.Generic;
using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Modal;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.Modal;

public class ModalFooterTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_Render_As_Div()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalFooterTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalFooterTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("modal-footer", output.Attributes["class"].Value);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} modal-footer";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("class", customClass)});
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new ModalFooterTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }
}