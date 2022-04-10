using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ICG.AspNetCore.Utilities.BootstrapTagHelpers.Form
{
    /// <summary>
    ///     TagHelper for rending Bootstrap form compliant textarea controls with support for ASP.NET Core model Binding.  Will
    ///     include Label, Field, and validation.
    /// </summary>
    [ExcludeFromCodeCoverage] //Excluding from code coverage due to complexity 
    [RestrictChildren("form-note")]
    public class FormTextAreaTagHelper : TextAreaTagHelper
    {
        private readonly IHtmlGenerator _generator;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="generator">Html Generator for field generation</param>
        public FormTextAreaTagHelper(IHtmlGenerator generator) : base(generator)
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
            //The Microsoft implementation of the TextArea tag helper doesn't actually load sub-controls so we need to
            var childContent = output.GetChildContentAsync().Result.GetContent();

            //Call our base implementation
            base.Process(context, output);

            //Set our tag name
            output.TagName = "textarea";

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

            //Add child content if we have it
            if(!string.IsNullOrEmpty(childContent))
                output.PostElement.AppendHtml(childContent);

            //Close wrapping div
            output.PostElement.AppendHtml("</div>");
        }
    }
}
