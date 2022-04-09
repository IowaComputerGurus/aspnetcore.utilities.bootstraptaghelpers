using System.Collections.Generic;
using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Form;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests.Form;

public class FormNoteTagHelperTests : AbstractTagHelperTest
{
    [Fact]
    public async Task Should_Render_As_SmallTag_By_Default()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new FormNoteTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("small", output.TagName);
    }

    [Fact]
    public async Task Should_Render_As_CustomTag_If_Defined()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");
        var expectedTagName = "sup";

        //Act
        var helper = new FormNoteTagHelper {TagName = expectedTagName};
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedTagName, output.TagName);
    }

    [Fact]
    public async Task Should_Render_With_DefaultClasses_Added()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");
        var expectedClasses = "form-text text-muted";

        //Act
        var helper = new FormNoteTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClasses, output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public async Task Should_Render_With_ClassAdded_PreservingCustomClasses()
    {
        //Arrange
        var customClass = "testing-out";
        var expectedClass = $"{customClass} form-text text-muted";
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("class", customClass)});
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new FormNoteTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }

    [Fact]
    public async Task Should_Render_Without_Duplicate_Classes_IfManuallyAdded()
    {
        //Arrange
        var customClass = "text-muted";
        var expectedClass = "text-muted form-text"; //Will be turned around due to appending
        var existingAttributes = new TagHelperAttributeList(new List<TagHelperAttribute>
            {new("class", customClass)});
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ", existingAttributes);

        //Act
        var helper = new FormNoteTagHelper();
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal(expectedClass, output.Attributes["class"].Value.ToString());
    }
}