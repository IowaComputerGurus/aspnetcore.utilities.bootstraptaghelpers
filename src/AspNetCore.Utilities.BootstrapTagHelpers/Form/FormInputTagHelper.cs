using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Form;

/// <summary>
///     TagHelper for rending Bootstrap form compliant input controls with support for ASP.NET Core model Binding.  Will
///     include Label, Field, and validation.
/// </summary>
[RestrictChildren("form-note")]
public class FormInputTagHelper : InputTagHelper, IFormElementMixin
{
    /// <inheritdoc />
    public IHtmlGenerator HtmlGenerator { get; }

    /// <summary>
    ///     Public constructor that will receive the incoming generator to leverage existing Microsoft Tag Helpers
    /// </summary>
    /// <param name="generator"></param>
    public FormInputTagHelper(IHtmlGenerator generator) : base(generator)
    {
        HtmlGenerator = generator;
    }

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
        output.TagName = "input";

        //Add the form-control class
        output.AddClass("form-control", HtmlEncoder.Default);

        //Add before div
        this.StartFormGroup(output);

        //Generate our label
        this.AddLabel(output);

        //Now, add validation message AFTER the field
        this.AddValidationMessage(output);

        //Close wrapping div
        this.EndFormGroup(output);
    }
}