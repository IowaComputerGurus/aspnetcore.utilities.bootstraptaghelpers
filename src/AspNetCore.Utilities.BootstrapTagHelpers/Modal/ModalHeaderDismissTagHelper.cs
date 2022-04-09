using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Modal;

/// <summary>
///     A tag helper that adds a dismiss button to the header of a Modal
/// </summary>
[HtmlTargetElement("modal-header-dismiss", ParentTag = "modal-header")]
public class ModalHeaderDismissTagHelper : TagHelper
{
    /// <summary>
    ///     Renders the needed HTML for the dismiss button
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.Add("aria-label", "Close");
        output.Attributes.Add("data-dismiss", "modal");
        output.AddClass("close", HtmlEncoder.Default);
        output.Attributes.Add("type", "button");
        var icon = new TagBuilder("span");
        icon.Attributes.Add("aria-hidden", "true");
        icon.InnerHtml.SetHtmlContent("&times;");
        output.Content.AppendHtml(icon);
    }
}