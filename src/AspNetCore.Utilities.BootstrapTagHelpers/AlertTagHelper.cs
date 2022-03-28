using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers;

/// <summary>
///     Tag helper for generating Bootstrap alerts
/// </summary>
public class AlertTagHelper : TagHelper
{
    /// <summary>
    ///     What style of alert should this be
    /// </summary>
    public BootstrapColor AlertColor { get; set; }

    /// <summary>
    ///     If set to true the element will not be shown
    /// </summary>
    public bool HideDisplay { get; set; }

    /// <summary>
    ///     Is this an alert that is dismissible
    /// </summary>
    public bool Dismissible { get; set; } = false;

    /// <summary>
    ///     Processes the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        //Stop render if hidden
        if (HideDisplay)
        {
            output.SuppressOutput();
            return;
        }

        //Add
        output.TagName = "div";
        output.Attributes.Add("class",
            Dismissible
                ? $"alert alert-{AlertColor.ToString().ToLower()} alert-dismissible fade show"
                : $"alert alert-{AlertColor.ToString().ToLower()}");
        output.Attributes.Add("role", "alert");

        if (!Dismissible)
            return;

        var buttonBuilder = new TagBuilder("button");
        buttonBuilder.Attributes.Add("type", "button");
        buttonBuilder.Attributes.Add("class", "btn-close");
        buttonBuilder.Attributes.Add("data-bs-dismiss", "alert");
        buttonBuilder.Attributes.Add("aria-label", "Close");

        //Get existing content
        var existing = await output.GetChildContentAsync();
        output.Content.AppendHtml(existing.GetContent());
        output.Content.AppendHtml(buttonBuilder);
    }
}