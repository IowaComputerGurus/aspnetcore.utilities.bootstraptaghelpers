using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests;

public class AlertTagHelperTests : AbstractTagHelperTest
{
    [Theory]
    [InlineData(BootstrapColor.Primary, "alert alert-primary")]
    [InlineData(BootstrapColor.Secondary, "alert alert-secondary")]
    [InlineData(BootstrapColor.Success, "alert alert-success")]
    [InlineData(BootstrapColor.Warning, "alert alert-warning")]
    [InlineData(BootstrapColor.Info, "alert alert-info")]
    [InlineData(BootstrapColor.Danger, "alert alert-danger")]
    public async Task Should_Render_ProperClass(BootstrapColor color, string expectedClass)
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput("");

        //Act
        var helper = new AlertTagHelper {AlertColor = color};
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
        Assert.Equal(expectedClass, output.Attributes["class"].Value);
    }

    [Fact]
    public async Task Should_Render_With_RoleAdded()
    {
        //Arrange
        var context = MakeTagHelperContext();
        var output = MakeTagHelperOutput(" ");

        //Act
        var helper = new AlertTagHelper {AlertColor = BootstrapColor.Info};
        await helper.ProcessAsync(context, output);

        //Assert
        Assert.Equal("div", output.TagName);
        Assert.Equal("alert", output.Attributes["role"].Value);
    }
}