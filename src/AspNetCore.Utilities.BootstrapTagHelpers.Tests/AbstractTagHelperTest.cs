using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Tests;

public abstract class AbstractTagHelperTest
{
    public static TagHelperContext MakeTagHelperContext(TagHelperAttributeList attributes = null)
    {
        attributes = attributes ?? new TagHelperAttributeList();

        return new TagHelperContext(
            "div",
            attributes,
            new Dictionary<object, object>(),
            Guid.NewGuid().ToString("N"));
    }

    public static TagHelperOutput MakeTagHelperOutput(
        string tagName,
        TagHelperAttributeList attributes = null,
        string childContent = null)
    {
        attributes = attributes ?? new TagHelperAttributeList();

        return new TagHelperOutput(
            tagName,
            attributes,
            (useCachedResult, encoder) =>
            {
                var tagHelperContent = new DefaultTagHelperContent();
                tagHelperContent.SetContent(childContent);
                return Task.FromResult<TagHelperContent>(tagHelperContent);
            });
    }

}