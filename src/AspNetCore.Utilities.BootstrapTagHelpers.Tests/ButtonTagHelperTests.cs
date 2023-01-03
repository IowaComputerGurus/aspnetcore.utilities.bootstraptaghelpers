using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit.Abstractions;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests;

[UsesVerify]
public class ButtonTagHelperTests : LoggingTagHelperTest
{
    public ButtonTagHelperTests(ITestOutputHelper output) : base(output)
    {
        
    }

    [Fact]
    public async Task Should_Not_Render_If_HideDisplay_Is_True()
    {
        var output = await (new ButtonTagHelper() { HideDisplay = true }).Render();
        Assert.True(output.Content.IsEmptyOrWhiteSpace);
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Should_Emit_Button_By_Default()
    {
        var output = await (new ButtonTagHelper()).Render();
        Assert.Equal("button", output.TagName);
        output.AssertContainsClass("btn");
        await VerifyTagHelper(output);
    }

    [Theory]
    [InlineData(ButtonType.Button, "button")]
    [InlineData(ButtonType.Submit, "submit")]
    [InlineData(ButtonType.Reset, "reset")]
    public async Task Properly_Sets_Button_Type(ButtonType buttonType, string expectedValue)
    {
        var output = await (new ButtonTagHelper() { Type = buttonType }).Render();
        Assert.Equal("button", output.TagName);
        Assert.Equal(expectedValue, output.Attributes["type"].Value.ToString());
        await VerifyTagHelper(output).UseParameters(buttonType);
    }

    [Theory]
    [InlineData(BootstrapColor.Info, "btn-info")]
    [InlineData(BootstrapColor.Success, "btn-success")]
    [InlineData(BootstrapColor.Danger, "btn-danger")]
    [InlineData(BootstrapColor.Warning, "btn-warning")]
    public async Task Properly_Sets_Btn_Class(BootstrapColor color, string expected)
    {
        var output = await (new ButtonTagHelper() { Color = color }).Render();
        output.AssertContainsClass(expected);
        await VerifyTagHelper(output).UseParameters(color);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Value_Is_Not_Emitted_If_Value_Is_Null_Or_Empty(string value)
    {
        var output = await (new ButtonTagHelper() { Value = value }).Render();
        Assert.DoesNotContain(output.Attributes, a => a.Name == "value");
        await VerifyTagHelper(output).UseParameters(value);
    }

    [Fact]
    public async Task Value_Attribute_Is_Set_If_Value_Has_Value()
    {
        var output = await (new ButtonTagHelper() { Value = "value" }).Render();
        Assert.Equal("value", output.Attributes["value"].Value.ToString());
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Emits_Disabled_Attribute_If_Disabled()
    {
        var output = await (new ButtonTagHelper() { Disabled = true }).Render();
        Assert.Contains(output.Attributes, a => a.Name == "disabled");
        await VerifyTagHelper(output);
    }

    [Theory]
    [InlineData(ButtonSize.Small, "btn-sm")]
    [InlineData(ButtonSize.Large, "btn-lg")]
    public async Task Sets_Button_Size_If_Not_Normal(ButtonSize size, string expected)
    {
        var output = await (new ButtonTagHelper() { Size = size }).Render();
        output.AssertContainsClass(expected);
        await VerifyTagHelper(output).UseParameters(size);
    }

    [Fact]
    public async Task Adds_Class_If_Block_Button()
    {
        var output = await (new ButtonTagHelper() { Block=true }).Render();
        output.AssertContainsClass("btn-block");
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Renders_Child_Content()
    {
        var output = await (new ButtonTagHelper()).Render(childContent: new HtmlString("<i class='fas fa-trash'></i>Trash"));
        await VerifyTagHelper(output);
    }

    [Fact]
    public async Task Omits_End_Tag_If_No_Content()
    {
        var output = await (new ButtonTagHelper()).Render();

        Assert.Equal(TagMode.SelfClosing, output.TagMode);
        await VerifyTagHelper(output);
    }
}

