using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Card;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Contexts;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.Card;

public class CardHeaderTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_ThrowException_WhenMissingContext()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardHeaderTagHelper();
        var exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        Assert.NotNull(exceptionResult);
        Assert.IsType<KeyNotFoundException>(exceptionResult);
    }

    [Fact]
    public async Task Should_ThrowException_WhenContextIsNull()
    {
        //Arrange
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(CardContext), null);
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardHeaderTagHelper();
        var exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        Assert.NotNull(exceptionResult);
        Assert.IsType<ArgumentException>(exceptionResult);
    }

    [Fact]
    public async Task Should_Render_As_Div_WhenContextAvailable()
    {
        //Arrange
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(CardContext), new CardContext());
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded()
    {
        //Arrange
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(CardContext), new CardContext());
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("card-header", output.Attributes["class"].Value);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} card-header";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("class", customClass)});
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(CardContext), new CardContext());
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new CardHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }
    
    [Theory]
    [InlineData("", "", "<div class=\"d-md-flex align-items-center w-100\"></div>")]
    [InlineData("Testing", "", "<div class=\"d-md-flex align-items-center w-100\"><h5>Testing</h5></div>")]
    [InlineData("Testing", "myCard", "<div class=\"d-md-flex align-items-center w-100\"><h5 id=\"myCardLabel\">Testing</h5></div>")]
    public async Task Should_Render_WithProper_InnerHtmlContent(string title, string contextId, string expectedOutput)
    {
        //Arrange
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(CardContext), new CardContext{Id = contextId});
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new CardHeaderTagHelper{Title = title};
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedOutput, output.Content.GetContent());
    }
}