using System.Collections.Generic;
using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Card;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.Card;

public class CardTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_Render_As_Div()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardTagHelper();
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
        var helper = new CardTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("card", output.Attributes["class"].Value);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} card";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("class", customClass)});
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new CardTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }
}