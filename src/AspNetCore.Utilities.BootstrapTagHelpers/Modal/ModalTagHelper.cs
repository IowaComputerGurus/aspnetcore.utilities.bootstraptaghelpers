﻿using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ICG.AspNetCore.Utilities.BootstrapTagHelpers.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Modal;

/// <summary>
///     A high-level wrapper Tag Helper for rendering a bootstrap Modal
/// </summary>
[RestrictChildren("modal-body", "modal-header", "modal-footer")]
public class ModalTagHelper : TagHelper
{
    /// <summary>
    ///     If set to true the background will not be clickable to dismiss the dialog
    /// </summary>
    public bool StaticBackdrop { get; set; } = false;

    /// <summary>
    ///     Renders the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        //Obtain the id value to add to the context
        var id = "";
        if (output.Attributes.ContainsName("id"))
            id = output.Attributes["id"].Value.ToString();

        //Add the id to the context
        var modalContext = new ModalContext { Id = id };
        context.Items[typeof(ModalContext)] = modalContext;

        //Get our child content before we mess with anything
        var body = (await output.GetChildContentAsync()).GetContent();
        body = body.Trim();

        output.TagName = "div";

        //Add classes to the existing tag, merging with custom ones added
        output.AddClass("modal", HtmlEncoder.Default);
        output.AddClass("fade", HtmlEncoder.Default);
        
        if(!string.IsNullOrEmpty(id))
            output.Attributes.Add("aria-labelledby", $"{id}Label");

        if (StaticBackdrop)
            output.Attributes.Add("data-backdrop", "static");

        var dialogWrapper = new TagBuilder("div");
        dialogWrapper.AddCssClass("modal-dialog");
        var dialogContent = new TagBuilder("div");
        dialogContent.AddCssClass("modal-content");
        dialogContent.InnerHtml.AppendHtml(body);
        dialogWrapper.InnerHtml.AppendHtml(dialogContent);

        output.Content.AppendHtml(dialogWrapper);
    }
}