using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Xunit;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests;

public static class TagHelperExtensions
{
    public static async Task<TagHelperOutput> Render(this TagHelper helper)
    {
        var context = AbstractTagHelperTest.MakeTagHelperContext();
        var output = AbstractTagHelperTest.MakeTagHelperOutput("");

        await helper.ProcessAsync(context, output);
        
        return output;
    }

    public static string Render(this TagHelperOutput tagOutput)
    {
        var writer = new StringWriter();
        var encoder = HtmlEncoder.Create();
        tagOutput.WriteTo(writer, encoder);

        return writer.ToString();
    }

    public static void AssertContainsClass(this TagHelperOutput output, string className)
    {
        var tagClasses = output.Attributes["class"].Value.ToString()?.Split(' ');
        Assert.Contains(tagClasses, s => s == className);
    }
}