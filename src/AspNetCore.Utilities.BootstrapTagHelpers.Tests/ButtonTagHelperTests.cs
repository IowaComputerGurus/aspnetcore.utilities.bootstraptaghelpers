using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests;

public class ButtonTagHelperTests : AbstractTagHelperTest
{
    private readonly ITestOutputHelper _output;

    public ButtonTagHelperTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task Should_Not_Render_If_HideDisplay_Is_True()
    {
        var output = await (new ButtonTagHelper() { HideDisplay = true }).Render();
        _output.WriteLine($"Output: {output.Render()}");
        Assert.True(output.Content.IsEmptyOrWhiteSpace);
    }

    [Fact]
    public async Task Should_Emit_Button_By_Default()
    {
        var output = await (new ButtonTagHelper()).Render();
        _output.WriteLine($"Output: {output.Render()}");
        Assert.Equal("button", output.TagName);
        output.AssertContainsClass("btn");
    }

    [Theory]
    [InlineData(ButtonType.Button, "button")]
    [InlineData(ButtonType.Submit, "submit")]
    [InlineData(ButtonType.Reset, "reset")]
    public async Task Properly_Sets_Button_Type(ButtonType buttonType, string expectedValue)
    {
        var output = await (new ButtonTagHelper() { Type = buttonType }).Render();
        _output.WriteLine($"Output: {output.Render()}");

        Assert.Equal("button", output.TagName);
        Assert.Equal(expectedValue, output.Attributes["type"].Value.ToString());
    }

    [Theory]
    [InlineData(BootstrapColor.Info, "btn-info")]
    [InlineData(BootstrapColor.Success, "btn-success")]
    [InlineData(BootstrapColor.Danger, "btn-danger")]
    [InlineData(BootstrapColor.Warning, "btn-warning")]
    public async Task Properly_Sets_Btn_Class(BootstrapColor color, string expected)
    {
        var output = await (new ButtonTagHelper() { Color = color }).Render();
        _output.WriteLine($"Output: {output.Render()}");
        output.AssertContainsClass(expected);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Value_Is_Not_Emitted_If_Value_Is_Null_Or_Empty(string value)
    {
        var output = await (new ButtonTagHelper() { Value = value }).Render();
        _output.WriteLine($"Output: {output.Render()}");

        Assert.DoesNotContain(output.Attributes, a => a.Name == "value");
    }

    [Fact]
    public async Task Value_Attribute_Is_Set_If_Value_Has_Value()
    {
        var output = await (new ButtonTagHelper() { Value = "value" }).Render();
        _output.WriteLine($"Output: {output.Render()}");
        Assert.Equal("value", output.Attributes["value"].Value.ToString());
    }

    [Fact]
    public async Task Emits_Disabled_Attribute_If_Disabled()
    {
        var output = await (new ButtonTagHelper() { Disabled = true }).Render();
        _output.WriteLine($"Output: {output.Render()}");
        Assert.Contains(output.Attributes, a => a.Name == "disabled");
    }

    [Theory]
    [InlineData(ButtonSize.Small, "btn-sm")]
    [InlineData(ButtonSize.Large, "btn-lg")]
    public async Task Sets_Button_Size_If_Not_Normal(ButtonSize size, string expected)
    {
        var output = await (new ButtonTagHelper() { Size = size }).Render();
        _output.WriteLine($"Output: {output.Render()}");
        output.AssertContainsClass(expected);
    }

    [Fact]
    public async Task Adds_Class_If_Block_Button()
    {
        var output = await (new ButtonTagHelper() { Block=true }).Render();
        _output.WriteLine($"Output: {output.Render()}");
        output.AssertContainsClass("btn-block");
    }
}

