using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Form;

/// <summary>
///     TagHelper for rending Bootstrap form compliant input controls with support for ASP.NET Core model Binding.  Will
///     include Label, Field, and validation.
/// </summary>
[RestrictChildren("form-note")]
public class FormInputTagHelper : InputTagHelper
{
    private readonly IHtmlGenerator _generator;
    
    /// <summary>
    ///     Public constructor that will receive the incoming generator to leverage existing Microsoft Tag Helpers
    /// </summary>
    /// <param name="generator"></param>
    public FormInputTagHelper(IHtmlGenerator generator) : base(generator)
    {
        _generator = generator;
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
        output.PreElement.AppendHtml("<div class=\"form-group\">");

        //Generate our label
        var label = _generator.GenerateLabel(
            ViewContext,
            For.ModelExplorer,
            For.Name, null,
            new { @class = "control-label" });
        output.PreElement.AppendHtml(label);
        

        //Now, add validation message AFTER the field
        var validationMsg = _generator.GenerateValidationMessage(
            ViewContext,
            For.ModelExplorer,
            For.Name,
            null,
            ViewContext.ValidationMessageElement,
            new { @class = "text-danger" });
        output.PostElement.AppendHtml(validationMsg);
        
        //Close wrapping div
        output.PostElement.AppendHtml("</div>");
    }
}