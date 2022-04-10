using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Contexts;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Modal;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.Modal;

public class ModalHeaderTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_ThrowException_WhenMissingContext()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderTagHelper();
        var exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        Assert.NotNull(exceptionResult);
        Assert.IsType<KeyNotFoundException>(exceptionResult);
    }

    [Fact]
    public async Task Should_ThrowException_WhenContextIsNull()
    {
        //Arrange
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), null);
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderTagHelper();
        var exceptionResult = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        Assert.NotNull(exceptionResult);
        Assert.IsType<ArgumentException>(exceptionResult);
    }

    [Fact]
    public async Task Should_Render_As_Div()
    {
        //Arrange
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), new ModalContext());
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded()
    {
        //Arrange
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), new ModalContext());
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("modal-header", output.Attributes["class"].Value);
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} modal-header";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("class", customClass)});
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), new ModalContext());
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new ModalHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public async Task Should_NotRender_InnerContent_When_Title_Missing()
    {
        //Arrange
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), new ModalContext());
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("", output.Content.GetContent());
    }
    
    [Theory]
    [InlineData("My Title", "", "<h5 class=\"modal-title\">My Title</h5>")]
    [InlineData("My Title", "myModal", "<h5 class=\"modal-title\" id=\"myModalLabel\">My Title</h5>")]
    public async Task Should_Render_InnerContent_Title_When_Title_Provided(string title, string id, string expectedHtml)
    {
        //Arrange
        var context = MakeTagHelperContext();
        context.Items.Add(typeof(ModalContext), new ModalContext{Id = id});
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalHeaderTagHelper{Title = title};
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedHtml, output.Content.GetContent());
    }
}