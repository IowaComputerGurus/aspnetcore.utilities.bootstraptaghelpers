using System;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers;
#nullable enable

/// <summary>
///     Types of button
/// </summary>
public enum ButtonType
{
    /// <summary>
    ///     A regular button
    /// </summary>
    Button,
    /// <summary>
    ///     A form submit button
    /// </summary>
    Submit,
    /// <summary>
    ///     A form reset button
    /// </summary>
    Reset
}

/// <summary>
///     Sizes of button
/// </summary>
public enum ButtonSize
{
    /// <summary>
    ///     Normal size
    /// </summary>
    Normal,
    /// <summary>
    ///     A large button
    /// </summary>
    Large,
    /// <summary>
    ///     A small button
    /// </summary>
    Small
}

/// <summary>
///     A tag helper for creating a Bootstrap button.
/// </summary>
[HtmlTargetElement("bs-button")]
public class ButtonTagHelper : TagHelper
{
    /// <summary>
    ///     What style of button to create.
    /// </summary>
    [HtmlAttributeName("bs-color")]
    public BootstrapColor Color { get; set; } = BootstrapColor.Info;

    /// <summary>
    ///     The type of button this will be
    /// </summary>
    public ButtonType Type { get; set; } = ButtonType.Button;

    /// <summary>
    ///     If set to true the element will not be shown
    /// </summary>
    public bool HideDisplay { get; set; }

    /// <summary>
    ///     The value of this button
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    ///     Is this button disabled
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    ///     Is this a block level button
    /// </summary>
    public bool Block { get; set; }

    /// <summary>
    ///     The size of this button
    /// </summary>
    public ButtonSize Size { get; set; } = ButtonSize.Normal;

    /// <inheritdoc/>
    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (HideDisplay)
        {
            output.SuppressOutput();
            return Task.CompletedTask;
        }
        output.TagName = "button";
        output.Attributes.Add("type", new HtmlString(Type.ToString().ToLowerInvariant()));
        output.AddClass("btn", HtmlEncoder.Default);
        output.AddClass($"btn-{Color.ToString().ToLowerInvariant()}", HtmlEncoder.Default);
        output.Attributes.Add("role", new HtmlString("button"));

        if (!string.IsNullOrEmpty(Value))
        {
            output.Attributes.Add("value", new HtmlString(Value));
        }

        if (Disabled)
        {
            output.Attributes.Add(new TagHelperAttribute("disabled"));
        }

        if (Size != ButtonSize.Normal)
        {
            output.AddClass(Size switch
            {
                ButtonSize.Large => "btn-lg",
                ButtonSize.Small => "btn-sm",
                ButtonSize.Normal => "",
                _ => throw new ArgumentOutOfRangeException(nameof(Size))
            }, HtmlEncoder.Default);
        }

        if (Block)
        {
            output.AddClass("btn-block", HtmlEncoder.Default);
        }
        return Task.CompletedTask;
    }
}

