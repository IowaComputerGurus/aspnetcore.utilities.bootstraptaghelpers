using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers;

/// <summary>
///     A tag helper for working with bootstrap Badges
/// </summary>
public class BadgeTagHelper : TagHelper
{
    /// <summary>
    ///     What style of badge should this be
    /// </summary>
    public BootstrapColor BadgeColor { get; set; }

    /// <summary>
    ///     If set to true the element will not be shown
    /// </summary>
    public bool HideDisplay { get; set; }

    /// <summary>
    ///     Processes the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //Stop render if hidden
        if (HideDisplay)
        {
            output.SuppressOutput();
            return;
        }

        //Add
        output.TagName = "span";
        output.AddClass("badge", HtmlEncoder.Default);
        output.AddClass($"badge-{BadgeColor.ToString().ToLower()}", HtmlEncoder.Default);
    }
}