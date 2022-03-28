using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.FontAwesomeTagHelpers;

/// <summary>
///     A tag helper for rendering a boolean value as a FontAwesome icon
/// </summary>
public class BoolFontAwesomeIconTagHelper : TagHelper
{
    /// <summary>
    ///     The value used to configure the tag helper
    /// </summary>
    public bool Value { get; set; }

    /// <summary>
    ///     The tag that should be used to render the icon
    /// </summary>
    /// <remarks>
    ///     Default value is "span" you may desire to switch to "i"
    /// </remarks>
    public string Tag { get; set; } = "span";

    /// <summary>
    ///     The full FontAwesome css class to be used for a true value
    /// </summary>
    /// <remarks>
    ///     Default value is "fas fa-check text-success"
    /// </remarks>
    public string TrueIconClass { get; set; } = "fas fa-check text-success";

    /// <summary>
    ///     The full FontAwesome css class to be used for a false value
    /// </summary>
    /// <remarks>
    ///     Default value is "fas fa-times text-danger"
    /// </remarks>
    public string FalseIconClass { get; set; } = "fas fa-times text-danger";

    /// <summary>
    ///     Renders the tag as desired
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = Tag;
        output.Attributes.Add("class", Value ? TrueIconClass : FalseIconClass);
    }
}