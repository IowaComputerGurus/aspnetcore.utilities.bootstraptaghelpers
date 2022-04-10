using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Contexts;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Modal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.Modal;

public class ModalTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_Not_Error_With_Missing_Id_Attribute()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalTagHelper();
        var exception = await Record.ExceptionAsync(() => helper.ProcessAsync(context, output));

        //Assert
        Assert.Null(exception);
    }

    [Fact]
    public async Task Should_Add_Context_Object_With_Provided_Id()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var providedId = "testingModal";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("id", providedId)});
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new ModalTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        var generatedContext = Assert.IsType<ModalContext>(context.Items[typeof(ModalContext)]);
        Assert.Equal(providedId, generatedContext.Id);
    }

    [Fact]
    public async Task Should_Render_As_Div()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalTagHelper();
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
        var helper = new ModalTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("modal fade", output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} modal fade";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("class", customClass)});
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new ModalTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }

    [Theory]
    [InlineData("", false, "")]
    [InlineData("testing", true, "testingLabel")]
    public async Task Should_Render_With_Aria_LabelledBy_When_Id_Provided(string providedId, bool expectedAttribute,
        string expectedAttributeValue)
    {
        //Arrange
        var context = MakeTagHelperContext();
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("id", providedId)});
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new ModalTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        if (expectedAttribute)
        {
            Assert.Equal(expectedAttributeValue, output.Attributes["aria-labelledby"].Value.ToString());
        }
        else
        {
            Assert.Null(output.Attributes["aria-labelledby"]);
        }
    }

    [Theory]
    [InlineData(true, "static")]
    [InlineData(false, "")]
    public async Task Should_Render_Static_Backdrop_Attribute_When_Flagged(bool staticBackdrop,
        string expectedAttributeValue)
    {
        //Arrange
        var attributeName = "data-backdrop";
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new ModalTagHelper{StaticBackdrop = staticBackdrop};
        await helper.ProcessAsync(context, output);

        //Assert
        if(staticBackdrop)
            Assert.Equal(expectedAttributeValue, output.Attributes[attributeName].Value.ToString());
        else
            Assert.Null(output.Attributes[attributeName]);
    }

    [Fact]
    public async Task Should_Render_Proper_InnerHtmlStructure_WithNoChildContent()
    {
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");
        var expectedContent = "<div class=\"modal-dialog\"><div class=\"modal-content\"></div></div>";

        //Act
        var helper = new ModalTagHelper { };
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedContent, output.Content.GetContent());
    }
}