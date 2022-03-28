using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers;

/// <summary>
///     TagHelper for rending Bootstrap form compliant input controls with support for ASP.NET Core model Binding.  Will
///     include Label, Field, and validation.
/// </summary>
public class FormTextInputTagHelper : TagHelper
{
    private readonly IHtmlGenerator _generator;

    /// <summary>
    ///     Public constructor that will receive the incoming generator to leverage existing Microsoft Tag Helpers
    /// </summary>
    /// <param name="generator"></param>
    public FormTextInputTagHelper(IHtmlGenerator generator)
    {
        _generator = generator;
    }

    /// <summary>
    ///     This maps to the existing standard of asp-for attribute/model binding
    /// </summary>
    [HtmlAttributeName("asp-for")]
    public ModelExpression For { get; set; }

    /// <summary>
    ///     Ensures that we have the proper context with the helper
    /// </summary>
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    /// <summary>
    ///     Used to actually process the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //Create wrapping element
        var elementBuilder = new TagBuilder("div");
        elementBuilder.Attributes.Add("class", "form-group");

        //Add the label
        var label = _generator.GenerateLabel(
            ViewContext,
            For.ModelExplorer,
            For.Name, null,
            new { @class = "control-label" });
        elementBuilder.InnerHtml.AppendHtml(label);

        //Add the textbox
        var textBox = _generator.GenerateTextBox(ViewContext,
            For.ModelExplorer,
            For.Name,
            For.Model,
            null,
            new { @class = "form-control" });
        elementBuilder.InnerHtml.AppendHtml(textBox);

        //Add validation messages
        var validationMsg = _generator.GenerateValidationMessage(
            ViewContext,
            For.ModelExplorer,
            For.Name,
            null,
            ViewContext.ValidationMessageElement,
            new { @class = "text-danger" });
        elementBuilder.InnerHtml.AppendHtml(validationMsg);

        //Set the element as the content for the helper
        output.Content.SetHtmlContent(elementBuilder);
    }
}