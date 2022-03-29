﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Modal;

[HtmlTargetElement("modal-footer", ParentTag = "modal")]
public class ModalFooterTagHelper : TagHelper
{
    /// <summary>
    ///     Completes the actual rendering of the Tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        //Setup basic tag information
        output.TagName = "div";
        output.Attributes.Add("class", "modal-footer");

        //Append other items, such as the dismiss button etc
        var body = (await output.GetChildContentAsync()).GetContent();
        body = body.Trim();
        output.Content.AppendHtml(body);
    }
}