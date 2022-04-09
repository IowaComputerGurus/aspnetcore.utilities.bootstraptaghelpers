using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Modal;

/// <summary>
///     Tag helper for toggling a modal's visibility
/// </summary>
public class ModalToggleTagHelper : TagHelper
{
    /// <summary>
    ///     The HTML id of the modal target
    /// </summary>
    public string Target { get; set; }

    /// <summary>
    ///     Defines the bootstrap color that should be used to render the button
    /// </summary>
    public BootstrapColor ToggleColor { get; set; } = BootstrapColor.Primary;

    /// <summary>
    ///     What type of tag should be rendered, by default it is a button
    /// </summary>
    public string TagName { get; set; } = "button";

    /// <summary>
    ///     Processes the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = TagName;
        output.Attributes.Add("data-toggle", "modal");
        output.Attributes.Add("data-target", $"#{Target}");
        output.AddClass("btn", HtmlEncoder.Default);
        output.AddClass($"btn-{ToggleColor.ToString().ToLower()}", HtmlEncoder.Default);

        if (TagName == "button")
            output.Attributes.Add("type", "button");
    }
}