using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Form;

/// <summary>
/// Custom implementation of the select tag helper
/// </summary>
public class FormSelectTagHelper : SelectTagHelper, IFormElementMixin
{
    /// <inheritdoc />
    public IHtmlGenerator HtmlGenerator { get; }

    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="generator">Html Generator for field generation</param>
    public FormSelectTagHelper(IHtmlGenerator generator) : base(generator)
    {
        HtmlGenerator = generator;
    }

    /// <summary>
    /// Allows the addition of a note to the field
    /// </summary>
    public string Note { get; set; }

    /// <summary>
    ///     Used to actually process the tag helper
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        //Call our base implementation
        base.Process(context, output);

        //Set our tag name
        output.TagName = "select";

        //Add the form-control class
        output.AddClass("form-control", HtmlEncoder.Default);
        //Add before div

        this.StartFormGroup(output);

        //Generate our label
        this.AddLabel(output);

        //Now, add validation message AFTER the field
        this.AddValidationMessage(output);

        if (!string.IsNullOrEmpty(Note))
            output.PostElement.AppendHtml($"<small class=\"form-text text-muted\">{Note}</small>");

        this.EndFormGroup(output);
    }
}
